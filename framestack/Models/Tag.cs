using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace framestack.Models
{
    public class Tag
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        public Tag(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

    }
}
