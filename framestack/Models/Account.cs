using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    public class Account
    {
        private DateTime dateCreated;
        private List<Picture> pictureList;
        private List<Video> videoList;
        private List<Album> albums;

        public Account(DateTime dateCreated, List<Album> albums)
        {
            this.dateCreated = dateCreated;
            this.pictureList = new List<Picture>();
            this.videoList = new List<Video>();
            this.albums = albums;
        }

        public void createPicture(Picture picture)
        {
            this.pictureList.Add(picture);
        }
 
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
                var picture = new Picture("TESTING", "No description", result.FileName);
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

        public async Task<bool> addVideo()
        {
            try
            {
                var result = await MediaPicker.PickVideoAsync();
                if(result != null)
                {
                    if(result.FileName.EndsWith("mp4", StringComparison.OrdinalIgnoreCase) || 
                       result.FileName.EndsWith("wmv", StringComparison.OrdinalIgnoreCase) || 
                       result.FileName.EndsWith("avi", StringComparison.OrdinalIgnoreCase) || 
                       result.FileName.EndsWith("flv", StringComparison.OrdinalIgnoreCase) || 
                       result.FileName.EndsWith("gifv", StringComparison.OrdinalIgnoreCase) ||
                       result.FileName.EndsWith("mp4", StringComparison.OrdinalIgnoreCase) || 
                       result.FileName.EndsWith("svi", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        var image = ImageSource.FromStream(() => stream);
                    } 
                    if(result == null)
                    {
                        return false;
                    }
                    var video = new Video("testing", null, result.FileName);
                    videoList.Add(video);
                    return true;
                }

            } 
            catch (Exception)
            {
                //
            }
            return false;
        }

        public void removeVideo(Video video)
        {
            this.videoList.Remove(video);
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

        public List<Video> getVideoList()
        {
            return this.videoList;
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
