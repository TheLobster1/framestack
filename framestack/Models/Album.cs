using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    public class Album
    {
        private string name;
        private string? description;
        private List<Picture> pictureList;
        private List<Video> videoList;

        public Album(string name, string? description)
        {
            this.name = name;
            this.description = description;
            this.pictureList = new List<Picture>();
            this.videoList = new List<Video>();
        }

        public void addPicture(Picture picture)
        {
            this.pictureList.Add(picture);
        }

        public void removePicture(Picture picture)
        {
            if (this.pictureList.Contains(picture)) 
            { 
                this.pictureList.Remove(picture);
            }
        }

        public void addVideo(Video video)
        {
            if (!this.videoList.Contains(video))
            {
                this.videoList.Add(video);
            }
        }

        public void removeVideo(Video video)
        {
            this.videoList.Remove(video);
        }

        public void addPictures(IEnumerable<Picture> newPictures)
        {
            foreach (Picture picture in newPictures)
            {
                if (!this.pictureList.Contains(picture))
                {
                    this.pictureList.Add(picture);
                }
            }
        }

        public void removePictures(List<Picture> toRemovePictures)
        {
            foreach (Picture picture in toRemovePictures)
            {
                this.pictureList.Remove(picture);
            }
        }

        public void addVideos(List<Video> videosToAdd)
        {
            foreach (Video video in videosToAdd)
            {
                if(!this.videoList.Contains(video))
                {
                    this.videoList.Add(video);
                }
            }
        }

        public void setDescription(string description)
        {
            this.description = description;
        }
        
       public string getDescription()
       {
            if (this.description == null)
            {
                return "No description";
            } else 
            { 
                return this.description;
            }
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
