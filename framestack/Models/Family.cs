using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    public class Family : Account
    {
        private string name;
        private List<User> members;

        public Family(string name, List<User> members, DateTime dateOfBirth, List<Album> albums) : base(albums)
        {
            this.name = name;
            this.members = members;
        }

        public void addMember(User user)
        {  
            this.members.Add(user);
        }
        public void removeMember(User user)
        {
            this.members.Remove(user);
        }

        public List<User> getMembers() 
        {  
            return this.members; 
        }


        public void setName(String name)
        {
            this.name = name;
        }

        public String getName() { 
            return this.name;
        }
    }
}
