using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace framestack.Models
{
    public class Family : Account
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("members")]
        public List<User> members { get; set; }

        public Family(string name, List<User> members, DateTime dateOfBirth)
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
