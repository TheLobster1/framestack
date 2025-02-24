using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    class User : Account
    {
        private string userName;
        private string passWord;
        private string firstName;
        private string lastName;
        private string eMail;

        public User(string userName, string passWord, string firstName, string lastName, string eMail, DateTime dateCreated, List<Media> media, List<Album> albums) : base(dateCreated, media, albums)
        {
            this.userName = userName;
            this.passWord = passWord;
            this.firstName = firstName;
            this.lastName = lastName;
            this.eMail = eMail;
        }
    }
}
