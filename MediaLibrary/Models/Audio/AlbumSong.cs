using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audio
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
        public int TrackNr { get; set; }
        [Required]
        [Display(Name = "Játékidő")]
        public DateTime PlayTime { get; set; }
        [Display(Name = "Lemez")]
        public Byte Disc { get; set; }
        [Display(Name = "Megjegyzés")]
        public string Note { get; set; }
    }
}
