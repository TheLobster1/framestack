using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Models
{
    class Account
    {
        private DateTime dateCreated;
        private List<Picture> pictureList;
        private List<Media> media;
        private List<Album> albums;

        public Account(DateTime dateCreated, List<Media> media, List<Album> albums)
        {
            this.dateCreated = dateCreated;
            this.pictureList = new List<Picture>();
            this.media = media;
            this.albums = albums;
        }

        public void createPicture(Picture picture)
        {
            this.pictureList.Add(picture);
        }

        public void deletePicture(Picture picture)
        {
            this.pictureList.Remove(picture);
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
