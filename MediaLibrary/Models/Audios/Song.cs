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
        [Display(Name = "Zeneszám címe")]
        public string SongTitle { get; set; }
        [Display(Name = "Szöveg")]
        [DataType(DataType.MultilineText)]
        public string SongLiryc { get; set; }
        public ICollection<PerformerSong> PerformerSongs { get; set; }

        public virtual ICollection<AlbumSong> AlbumSongs { get; set; }

    }
}
