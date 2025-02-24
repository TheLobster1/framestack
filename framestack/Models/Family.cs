using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    class Family : Account
    {
        private string name;
        private List<User> members;

        public Family(string name, List<User> members, DateTime dateCreated, List<Media> media, List<Album> albums) : base(dateCreated, media, albums)
        {
            this.name = name;
            this.members = members;
        }
    }
}
