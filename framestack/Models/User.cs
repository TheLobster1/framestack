using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    public class User : Account
    {
        private string userName;
        private string passWord;
        private string firstName;
        private string lastName;
        private string eMail;

        public User(string userName, string passWord, string firstName, string lastName, string eMail, DateTime dateCreated, List<Album> albums) : base(dateCreated, albums)
        {
            this.userName = userName;
            this.passWord = passWord;
            this.firstName = firstName;
            this.lastName = lastName;
            this.eMail = eMail;
        }

        public void setPassword(string password)
        {
            //The 13 is the "Work factor" aka how many times it has been hashed -> 2^(workfactor) are the iterations
            this.passWord = BCrypt.Net.BCrypt.EnhancedHashPassword(password,13);
        }
        public bool isPasswordCorrect(string password)
        {   //Checks whether the passwords are the same 
            return BCrypt.Net.BCrypt.EnhancedVerify(password, this.passWord);
        }

        public void setEmail(string email) 
        {
            this.eMail = email;
        }

        public void setFirstName(string firstName) 
        { 
            this.firstName = firstName;
        }

        public void setLastName(string lastName)
        {
            this.lastName = lastName;
        }

        public void setUserName(string userName)
        {
            this.userName = userName;
        }

    }
}
