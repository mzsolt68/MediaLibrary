using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audios
{
    public class Song
    {
        public int SongID { get; set; }
        [Required]
        public string SongTitle { get; set; }
        public string SongLiryc { get; set; }
        public ICollection<PerformerSong> PerformerSongs { get; set; }

        public virtual ICollection<AlbumSong> AlbumSongs { get; set; }

    }
}
