using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audios
{
    public class AlbumSong
    {
        [Required]
        public int AlbumID { get; set; }
        public Album Album { get; set; }
        [Required]
        public int SongID { get; set; }
        public Song Song { get; set; }
        [Required]
        public DateTime PlayTime { get; set; }
        public string Note { get; set; }
    }
}
