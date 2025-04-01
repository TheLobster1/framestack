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
            command.CommandText = $@"SELECT COUNT(*) as count_user FROM useraccount where Email = @Email;";
            command.Parameters.AddWithValue("@Email", Email);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var count_user = reader.GetInt32("count_user");
                userExists = count_user != 0;
            }
            if (userExists)
            {
                _connection.Close();
                return "Email already being used";
            }
            command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = $@"INSERT INTO `account` (`Id`, `Created`) VALUES (NULL, NOW());";
            var accountReader = command.ExecuteReader();
            var accountId = -1;
            while (accountReader.Read())
            {
                accountId = accountReader.GetInt32("id");
            }
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
            var userReader = command.ExecuteReader();
            string firstName = null;
            string lastName = null;
            string userName = null;
            while (userReader.Read())
            {
                firstName = userReader.GetString("Firstname");
                lastName = userReader.GetString("Lastname");
                userName = userReader.GetString("Username");
            }
            _connection.Close();
            return $"Hi{firstName} {lastName}! The account with username {userName} has been created.";
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