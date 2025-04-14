using System.Net.Mail;
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
        string connectionString = "server="+_databaseServer+";uid="+_databaseUser+";pwd="+_databasePassword+";database="+_databaseName;     //database connection string with configured variables
        MySqlConnection _connection = new MySqlConnection(connectionString);
        try
        {
            _connection.Open(); //open connection to database
            bool userExists = false;
            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = $@"SELECT COUNT(*) AS count_user FROM useraccount WHERE Email = @Email OR Username = @Username;"; //get the amount of entries from the useraccount table with the email or username provided.
            command.Parameters.AddWithValue("@Email", email);   //add value to command
            command.Parameters.AddWithValue("@Username", userName); //add value to command
            var reader = command.ExecuteReader();   //start the query
            while (reader.Read())   //read the query which returns one row and one column
            {
                var count_user = reader.GetInt32("count_user");
                userExists = count_user != 0;   //if user_count is not 0 set to false, else set to true
            }
            reader.Close(); //close the reader
            if (userExists)
            {
                _connection.Close();    //close the connection
                return "Email already being used";
            }
            command = new MySqlCommand();   //start new command
            command.Connection = _connection;
            command.CommandText = $@"INSERT INTO `account` (`Id`, `Created`) VALUES (NULL, NOW());";    //create new account in database with auto incremented ID and creation date of now.
            command.ExecuteNonQuery();
            var accountId = command.LastInsertedId;     //COULD BE FAULTY DUE TO MULTIPLE USERS AT SAME TIME
            if (accountId == -1)
            {
                _connection.Close();    //close connection if accountId for some reason turns to -1
                return "Something went wrong";
            }
            command = new MySqlCommand();   //start another new command
            command.Connection = _connection;
            command.CommandText = $@"INSERT INTO `useraccount` (`Id`, `UserName`, `FirstName`, `LastName`, `DateOfBirth`, `Email`, `Password`) VALUES (@accountId, @Username, @Firstname, @Lastname, @BirthDate, @Email, @Password);";  
            command.Parameters.AddWithValue("@accountId", accountId);
            command.Parameters.AddWithValue("@Username", userName);
            command.Parameters.AddWithValue("@Firstname", firstName);
            command.Parameters.AddWithValue("@Lastname", lastName);
            command.Parameters.AddWithValue("@BirthDate", dob);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", passWord);
            //create new user with previously created accountId as foreign key and other values
            command.ExecuteNonQuery();
            _connection.Close();
            return $"Hi{firstName} {lastName}! The account with username {userName} has been created.";
        }
        catch (MySqlException ex)
        {
            // _connection.Close();
            Console.WriteLine(ex.Message);
            //An SQL query did not succeed
        }
        return "???";
    }
    
    public static async Task<List<Picture>> GetPicturesFromAccount(string email, int page = 0, int size = 20)
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
                                        -- This part concatenates the tag names.
                                        GROUP_CONCAT(t.name SEPARATOR ',') AS Tags
                                    FROM
                                        picture p
                                    LEFT JOIN
                                        picturetagconnect ptc ON p.ID = ptc.PictureId
                                    LEFT JOIN
                                        tag t ON ptc.TagId = t.ID
                                    WHERE
                                        p.AccountId = (SELECT Id FROM useraccount WHERE Email = @Email)
                                    GROUP BY
                                        p.Id,  -- Group by all picture columns to get one row per picture
                                        p.Name,
                                        p.Description,
                                        p.File,
                                        p.DateCreated,
                                        p.AccountId
                                    ORDER BY
                                        p.DateCreated DESC LIMIT {size} OFFSET {page * size}; -- order by date desc to get newest first. Limit to expected amount of items
                                    ";
            command.Parameters.AddWithValue("@Email", email);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var Id = reader.GetInt32("PictureId");
                var Name = reader.GetString("PictureName");
                var Description = reader.GetString("PictureDescription");
                var DateCreated = reader.GetDateTime("PictureCreationDate");
                var File = reader.GetString("PictureFilePath");
                var AccountId = reader.GetInt32("AccountId");
                string Tags = "";
                try
                {
                    Tags = reader.GetString("Tags");
                }
                catch (Exception ex)
                {
                    //IGNORE
                }
                var TagList = new List<Tag>();
                if (!string.IsNullOrEmpty(Tags))
                {
                    var tags = Tags.Split(','); //split tags as they were concatenated
                    foreach (var tag in tags)
                    {
                        TagList.Add(new Tag(tag));
                    }
                }
                Picture picture = new(File, Name, Description, Id, DateCreated, AccountId, TagList);
                pictures.Add(picture); //add picture to the list of pictures
            }
            _connection.Close();
            return pictures;
        }
        catch (Exception ex)
        {
            _connection.Close();
            Console.WriteLine(ex.Message);
            //some database operation did not work correctly.
        }
        return new List<Picture>(); //if something broke, an empty list will be returned
    }

    public static async Task<bool> CreatePicture(string name, string description, string filePath, string email)
    {
        string connectionString = "server=" + _databaseServer + ";uid=" + _databaseUser + ";pwd=" + _databasePassword +
                                  ";database=" + _databaseName;
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            var command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = $@"SELECT Id FROM useraccount WHERE Email = @Email";  //get ID from account with specified email
            command.Parameters.AddWithValue("@Email", email);
            var reader = command.ExecuteReader();
            int Id = -1;
            while (reader.Read())
            {
                Id = reader.GetInt32("Id"); //set ID to id from database if account exists
            }
            reader.Close();
            if (Id == -1) return false; //return false if account does not exist
            command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = $@"INSERT INTO `picture` (`Id`, `DateCreated`, `Name`, `Description`, `File`, `AccountId`) VALUES (NULL, NOW(), @Name, @Description, @File, @Id);"; //add picture with creation datetime of now
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@File", filePath);
            command.Parameters.AddWithValue("@Id", Id);
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

    public static async Task<string> GetPasswordHash(string loginCredential)
    {
        string connectionString = "server="+_databaseServer+";uid="+_databaseUser+";pwd="+_databasePassword+";database="+_databaseName;
        MySqlConnection _connection = new MySqlConnection(connectionString);
        try
        {
            _connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            if (MailAddress.TryCreate(loginCredential, out MailAddress mail)) //check if provided string is email or username
            {
                command.CommandText = $@"SELECT Password FROM useraccount WHERE Email = @LoginCredential;"; //if email get password where email is email
            }
            else
            {
                command.CommandText = $@"SELECT Password FROM useraccount WHERE UserName = @LoginCredential;"; //if not email get passwrod where username is username
            }
            
            command.Parameters.AddWithValue("@LoginCredential", loginCredential); 
            var reader = command.ExecuteReader();
            var passwordHash = "";
            while (reader.Read())
            {
                passwordHash = reader.GetString("Password");
            }
            _connection.Close();
            return passwordHash;
        }
        catch (Exception ex)
        {
            _connection.Close();
            Console.WriteLine(ex.Message);
            //Ignore
        }
        return "";
    }

    public static async Task<List<string>> CheckUser(string username, string email)
    {
        string connectionString = "server="+_databaseServer+";uid="+_databaseUser+";pwd="+_databasePassword+";database="+_databaseName;
        MySqlConnection _connection = new MySqlConnection(connectionString);
        var messages = new List<string>(); //new list of error messages to return
        try
        {
            _connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = $@"SELECT * FROM useraccount WHERE Email = @Email OR Username = @Username;";
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Username", username);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var tempEmail = reader.GetString("Email");
                var tempUsername = reader.GetString("Username");
                if (tempEmail == email) //if mail exists
                {
                    messages.Add($"Email {tempEmail} is already in use");
                }
                if (tempUsername == username)   //if username exists
                {
                    messages.Add($"Username {tempUsername} is already in use");
                }
            }
            _connection.Close();
            return messages;
        }
        catch (Exception ex)
        {
            _connection.Close();
            Console.WriteLine(ex.Message);
            //Ignore
        }

        return messages; //return messages
    }
    
    public static async Task<User?> GetUserDetails(string loginCredential)
    {
        string connectionString = "server="+_databaseServer+";uid="+_databaseUser+";pwd="+_databasePassword+";database="+_databaseName;
        MySqlConnection _connection = new MySqlConnection(connectionString);
        try
        {
            _connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            if (MailAddress.TryCreate(loginCredential, out MailAddress mail))
            {
                command.CommandText = $@"SELECT * FROM useraccount WHERE Email = @LoginCredential";
            }
            else
            {
                command.CommandText = $@"SELECT * FROM useraccount WHERE UserName = @LoginCredential";
            }
            command.Parameters.AddWithValue("@LoginCredential", loginCredential);
            var reader = command.ExecuteReader();
            User? user = null;
            while (reader.Read())
            {
                var tempEmail = reader.GetString("Email");
                var tempUsername = reader.GetString("Username");
                var tempFirstName = reader.GetString("FirstName");
                var tempLastName = reader.GetString("LastName");
                var tempDateOfBirth = reader.GetDateTime("DateOfBirth");
                user = new User(tempUsername, "", tempDateOfBirth, tempEmail, tempFirstName, tempLastName, [], []); //create new user to send back to application
            }
            _connection.Close();
            return user;
        }
        catch (Exception ex)
        {
            _connection.Close();
            Console.WriteLine(ex.Message);
            //Ignore
        }

        return null;    //return null if something went wrong
    }


}