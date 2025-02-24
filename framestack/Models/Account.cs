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
        private List<Media> media;
        private List<Album> albums;

        public Account(DateTime dateCreated, List<Media> media, List<Album> albums)
        {
            this.dateCreated = dateCreated;
            this.media = media;
            this.albums = albums;
        }
    }
}
