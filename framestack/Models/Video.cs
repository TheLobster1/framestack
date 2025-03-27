using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    class Video
    {
        private string name;
        private string? description;
        private string filePath;
        private List<Tag> tags;

        public Video(string name, string? description, string filePath)
        {
            this.name = name;
            this.description = description;
            this.filePath = filePath;
            this.tags = new List<Tag> ();
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
        public string getFilePath()
        {
            return this.filePath;
        }

        public void addTag(Tag tag)
        {
            this.tags.Add(tag);
        }

        public void removeTag(Tag tag)
        {
            this.tags.Remove(tag);
        }

    }
}
