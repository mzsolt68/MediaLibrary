using MediaLibrary.Entities.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Models.Audio
{
    public class Song
    {
        public int SongID { get; set; }
        [Required]
        [Display(Name = "Zeneszám címe")]
        public string SongTitle { get; set; }
        [Display(Name = "Szöveg")]
        [DataType(DataType.MultilineText)]
        public string SongLyric { get; set; }
        public int GenreID { get; set; }
        [Display(Name = "Műfaj")]
        public Genre Genre { get; set; }
        public int LanguageID { get; set; }
        [Display(Name = "Nyelv")]
        public Language Language { get; set; }

        public virtual List<PerformerSong> PerformerSongs { get; set; }

        public virtual ICollection<AlbumSong> AlbumSongs { get; set; }

    }
}
