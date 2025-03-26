using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    class Album
    {
        private string name;
        private string description;
        private List<Picture> pictures;

        public Album(string name)
        {
            this.name = name;
            this.pictures = new List<Picture>();
        }

        public Album(string name, string description)
        {
            this.name = name;
            this.description = description;
            this.pictures = new List<Picture>();
        }

        public void addPicture(Picture picture)
        {
            this.pictures.Add(picture);
        }

        public void removePicture(Picture picture)
        {
            if (this.pictures.Contains(picture)) 
            { 
            this.pictures.Remove(picture);
            }
        }

        public void addPictures(IEnumerable<Picture> newPictures)
        {
            foreach (Picture picture in newPictures)
            {
                if (!this.pictures.Contains(picture))
                {
                    this.pictures.Add(picture);
                }
            }
        }

        public void removePictures(List<Picture> toRemovePictures)
        {
            foreach (Picture picture in toRemovePictures)
            {
                this.pictures.Remove(picture);
            }
        }

        public void setDescription(string description)
        {
            this.description = description;
        }
        
       public string getDescription()
       {
            return this.description;
       }

        public string setName(string name)
        {
            return this.name;
        }

        public string getName()
        {
            return this.name;
        }
    }
}
