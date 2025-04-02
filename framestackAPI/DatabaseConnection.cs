using MySql.Data.MySqlClient;

namespace framestackAPI;

public static class DatabaseConnection
{
    private static string _databaseServer = "127.0.0.1";
    private static string _databaseUser = "root";
    private static string _databasePassword = "";
    private static string _databaseName = "framestack";

    public static async Task<string> CreateUser(string UserName, string PassWord, DateTime DOB, string Email, string FirstName, string LastName)
    {
        string connectionString = "server="+_databaseServer+";uid="+_databaseUser+";pwd="+_databasePassword+";database="+_databaseName;
        MySqlConnection _connection = new MySqlConnection(connectionString);
        try
        {
            _connection.Open();
            bool userExists = false;
            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = $@"SELECT COUNT(*) AS count_user FROM useraccount WHERE Email = @Email OR Username = @Username;";
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Username", UserName);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var count_user = reader.GetInt32("count_user");
                userExists = count_user != 0;
            }
            reader.Close();
            if (userExists)
            {
                _connection.Close();
                return "Email already being used";
            }
            command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = $@"INSERT INTO `account` (`Id`, `Created`) VALUES (NULL, NOW());";
            command.ExecuteNonQuery();
            var accountId = command.LastInsertedId;     //COULD BE FAULTY DUE TO MULTIPLE USERS AT SAME TIME
            if (accountId == -1)
            {
                _connection.Close();
                return "Something went wrong";
            }
            command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = $@"INSERT INTO `useraccount` (`Id`, `UserName`, `FirstName`, `LastName`, `DateOfBirth`, `Email`, `Password`) VALUES (@accountId, @Username, @Firstname, @Lastname, @BirthDate, @Email, @Password);";
            command.Parameters.AddWithValue("@accountId", accountId);
            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Firstname", FirstName);
            command.Parameters.AddWithValue("@Lastname", LastName);
            command.Parameters.AddWithValue("@BirthDate", DOB);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Password", PassWord);
            command.ExecuteNonQuery();
            _connection.Close();
            return $"Hi{FirstName} {LastName}! The account with username {UserName} has been created.";
        }
        catch (MySqlException ex)
        {
            // _connection.Close();
            Console.WriteLine(ex.Message);
            //Ignore
        }
        return "???";
    }
    
    public static async Task<List<Picture>> GetPicturesFromAccount(int accountId, int page = 0, int size = 20)
    {
        string connectionString = "server="+_databaseServer+";uid="+_databaseUser+";pwd="+_databasePassword+";database="+_databaseName;
        MySqlConnection _connection = new MySqlConnection(connectionString);
        var pictures = new List<Picture>();
        try
        {
            _connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = $@"SELECT * FROM picture where AccountId = @AccountId order by DateCreated LIMIT {size} OFFSET {page * size};";
            command.Parameters.AddWithValue("@AccountId", accountId);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var Id = reader.GetInt32("Id");
                var Name = reader.GetString("Name");
                var Description = reader.GetString("Description");
                var DateCreated = reader.GetDateTime("DateCreated");
                var File = reader.GetString("File");
                var AccountId = reader.GetInt32("AccountId");
                Picture picture = new(File, Name, Description, Id, DateCreated, AccountId);
                pictures.Add(picture);
            }
            _connection.Close();
            return pictures;
        }
        catch (Exception ex)
        {
            _connection.Close();
            Console.WriteLine(ex.Message);
            //Ignore
        }
        return new List<Picture>();
    }


}