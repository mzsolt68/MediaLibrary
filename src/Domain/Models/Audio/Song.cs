using Domain.Models.Common;
using SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Audio
{
    public class Song : Entity
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

        public virtual ICollection<PerformerSong> Performers { get; set; }

        public virtual ICollection<AlbumSong> Albums { get; set; }

    }
}
