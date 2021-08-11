using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MediaLibrary.Entities.Models.Audio
{
    public class AlbumSong
    {
        [Required]
        public int AlbumID { get; set; }
        [JsonIgnore]
        public Album Album { get; set; }
        [Required]
        public int SongID { get; set; }
        public Song Song { get; set; }
        [Required]
        public int TrackNr { get; set; }
        [Required]
        [Display(Name = "Játékidő")]
        public string PlayTime { get; set; }
        [Display(Name = "Lemez")]
        public Byte Disc { get; set; }
        [Display(Name = "Megjegyzés")]
        public string Note { get; set; }
    }
}
