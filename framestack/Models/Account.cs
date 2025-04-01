using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    public class Account
    {
        private List<Picture> pictureList;
        private List<Video> videoList;
        private List<Album> albums;

        public Account(List<Album> albums)
        {
            this.pictureList = new List<Picture>();
            this.videoList = new List<Video>();
            this.albums = albums;
        }

        public void createPicture(Picture picture)
        {
            this.pictureList.Add(picture);
        }

        //To test this 
        public async Task<bool> addPhoto()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("gif", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("svg", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        var image = ImageSource.FromStream(() => stream);
                    }
                }
                if(result == null)
                {
                    return false;
                }
                var picture = new Picture("TESTING", "No description", DateTime.Now, new List<Tag>(), result.FileName);
                pictureList.Add(picture);
                return true;
            }
            catch (Exception)
            {
               //
            }

            return false ;
        }

        public void deletePicture(Picture picture)
        {
            this.pictureList.Remove(picture);
        }

        public void createVideo(Video video)
        {
            this.videoList.Add(video);
        }

        public void removeVideo(Video video)
        {
            this.videoList.Remove(video);
        }

        public void addVideos(List<Video> videosToAdd)
        {
            foreach (Video v in videosToAdd)
            {
                if (!this.videoList.Contains(v))
                {
                    this.videoList.Add(v);
                }
            }
        }

        public void removeVideos(List<Video> videosToRemove)
        {
            foreach (Video v in videosToRemove)
            {
                if (this.videoList.Contains(v))
                {
                    this.videoList.Remove(v);
                }
            }
        }

        public void createAlbum(string name, string description)
        {
            this.albums.Add(new Album(name, description));
        }

        public void deleteAlbum(Album album)
        {
            this.albums.Remove(album);
        }
        
        public List<Album> getAlbums()
        {
            return this.albums;
        }

        public List<Picture> getPictureList()
        {
            return this.pictureList;
        }

        public void deletePictures(List<Picture> pictures)
        {
            foreach (Picture picture in pictures)
            {
                this.pictureList.Remove(picture);
            }
        }
    }
}
