using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audios
{
    public class Song
    {
        public int SongID { get; set; }
        public string SongTitle { get; set; }
        public string SongLiryc { get; set; }
        public ICollection<Performer> Performers { get; set; }

        public virtual ICollection<Album> AlbumsOfSong { get; set; }

    }
}
