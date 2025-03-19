using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Services
{
    public class DatabaseConnectionService
    {
        protected MySqlConnection _connection;
        protected readonly string _databaseServer = "127.0.0.1";
        protected readonly string _databaseUser = "root";
        protected readonly string _databasePassword = "";
        protected readonly string _databaseName = "framestack";

        public DatabaseConnectionService() 
        {
            string connectionString = "server="+_databaseServer+";uid="+_databaseUser+";pwd="+_databasePassword+";database="+_databaseName;
            _connection = new MySqlConnection(connectionString);
        }

        public async Task<string> CreateUser(string UserName, string PassWord, DateTime DOB, string Email, string FirstName, string LastName)
        {
            try
            {
                _connection.Open();
                //TODO: Check if user exists
                MySqlCommand command = new MySqlCommand();
                command.CommandText = @"SELECT * FROM useraccount where Email = @Email;";
                command.Parameters.AddWithValue("@Email", Email);
                using var reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {

                    }
                }
                _connection.Close();
            }
            catch (MySqlException ex)
            {
                _connection.Close();
                Console.WriteLine(ex.Message);
                //Ignore
            }
            return "";
        }


    }
}
