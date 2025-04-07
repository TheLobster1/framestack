using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace framestack.Models
{
     public class Picture
    {
        [JsonPropertyName("name")]
        public string Name {get; set;}
        [JsonPropertyName("description")]
        public string? Description {get; set;}
        [JsonPropertyName("tags")]
        public List<Tag> Tags {get; set;}
        [JsonPropertyName("dateCreated")]
        public DateTime DateCreated {get; set;}
        [JsonPropertyName("filePath")]
        public string FilePath {get; set;}

        public Picture(string name, string? description, DateTime dateCreated, List<Tag> tags, string filePath)
        {
            this.Name = name;
            this.Description = description;
            this.Tags = tags;
            this.FilePath = filePath;
            this.DateCreated = dateCreated;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public string GetName()
        {
            return this.Name;
        }

        public void SetDescription(string description)
        {
            this.Description = description;
        }

        public string GetDescription()
        {
            if (this.Description != null)
            {
                return this.Description;
            } else
            {
                return "No description";
            }
        }

        public void SetFilePath(string filePath)
        {
            this.FilePath = filePath;
        }

        public void AddTag(Tag tag)
        {
            if (!GetTags().Contains(tag))
            {
                Tags.Add(tag);
            }
            
        }

        public List<Tag> GetTags()
        {
            return Tags;
        }

    }
}
