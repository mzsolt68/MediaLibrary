using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audios
{
    public class Album
    {
        public int AlbumID { get; set; }
        public string AlbumTitle { get; set; }
        public AudioFormat AlbumFormat { get; set; }

        public virtual ICollection<Song> SongsOfAlbum { get; set; }

    }
}
