using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
     public class Picture
    {
        private string name;
        private string? description;
        private List<Tag> tags;
        private string filePath;

        public Picture(string name, string? description, string filePath)
        {
            this.name = name;
            this.description = description;
            this.tags = new List<Tag>();
            this.filePath = filePath;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        public string getDescription()
        {
            if (this.description != null)
            {
                return this.description;
            } else
            {
                return "No description";
            }
        }

        public void setFilePath(string filePath)
        {
            this.filePath = filePath;
        }

        public void addTag(Tag tag)
        {
            if (!getTags().Contains(tag))
            {
                tags.Add(tag);
            }
            
        }

        public List<Tag> getTags()
        {
            return tags;
        }

    }
}
