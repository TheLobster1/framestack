using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
     class Picture
    {
        private string name;
        private string description;
        private List<Tag> tags;
        private string filePath;

        public Picture(string filePath)
        {
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
