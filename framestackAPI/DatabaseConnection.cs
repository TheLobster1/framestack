using MySql.Data.MySqlClient;

namespace framestackAPI;

public static class DatabaseConnection
{
    private static string _databaseServer = "127.0.0.1";
    private static string _databaseUser = "root";
    private static string _databasePassword = "";
    private static string _databaseName = "framestack";

    public static async Task<string> CreateUser(string userName, string passWord, DateTime dob, string email, string firstName, string lastName)
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
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Username", userName);
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
            command.Parameters.AddWithValue("@Username", userName);
            command.Parameters.AddWithValue("@Firstname", firstName);
            command.Parameters.AddWithValue("@Lastname", lastName);
            command.Parameters.AddWithValue("@BirthDate", dob);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", passWord);
            command.ExecuteNonQuery();
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
            // command.CommandText = $@"SELECT * FROM picture where AccountId = @AccountId order by DateCreated LIMIT {size} OFFSET {page * size};";
            command.CommandText = $@"
                                    SELECT
                                        p.Id AS PictureID,
                                        p.Name AS PictureName,
                                        p.Description AS PictureDescription,
                                        p.File AS PictureFilepath,
                                        p.DateCreated AS PictureCreationDate,
                                        p.AccountId,
                                        -- This part concatenates the tag names. The specific function might vary
                                        -- depending on your SQL database system (see notes below).
                                        GROUP_CONCAT(t.name SEPARATOR ',') AS Tags
                                    FROM
                                        picture p
                                    LEFT JOIN
                                        picturetagconnect ptc ON p.ID = ptc.PictureId
                                    LEFT JOIN
                                        tag t ON ptc.TagId = t.ID
                                    WHERE
                                        p.AccountId = @AccountId -- <-- Replace :userId with the actual user account ID
                                    GROUP BY
                                        p.Id,  -- Group by all picture columns to get one row per picture
                                        p.Name,
                                        p.Description,
                                        p.File,
                                        p.DateCreated,
                                        p.AccountId
                                    ORDER BY
                                        p.DateCreated DESC LIMIT {size} OFFSET {page * size}; -- Optional: order pictures, e.g., by creation date
                                    ";
            command.Parameters.AddWithValue("@AccountId", accountId);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var Id = reader.GetInt32("PictureId");
                var Name = reader.GetString("PictureName");
                var Description = reader.GetString("PictureDescription");
                var DateCreated = reader.GetDateTime("PictureCreationDate");
                var File = reader.GetString("PictureFilePath");
                var AccountId = reader.GetInt32("AccountId");
                var Tags = reader.GetString("Tags");
                var TagList = new List<Tag>();
                if (!string.IsNullOrEmpty(Tags))
                {
                    var tags = Tags.Split(',');
                    foreach (var tag in tags)
                    {
                        TagList.Add(new Tag(tag));
                    }
                }
                Picture picture = new(File, Name, Description, Id, DateCreated, AccountId, TagList);
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

    public static async Task<bool> CreatePicture(string name, string description, string filePath, string accountId)
    {
        string connectionString = "server=" + _databaseServer + ";uid=" + _databaseUser + ";pwd=" + _databasePassword +
                                  ";database=" + _databaseName;
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            var command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = $@"INSERT INTO `picture` (`Id`, `DateCreated`, `Name`, `Description`, `File`, `AccountId`) VALUES (NULL, NULL, @Name, @Description, @File, @AccountId);";
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@File", filePath);
            command.Parameters.AddWithValue("@AccountId", accountId);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            connection.Close();
            Console.WriteLine(ex.Message);
            //Ignore
        }

        return false;
    }


}