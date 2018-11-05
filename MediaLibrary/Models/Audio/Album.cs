using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audio
{
    public class Album
    {
        public int AlbumID { get; set; }
        [Required]
        [Display(Name = "Album címe")]
        public string AlbumTitle { get; set; }
        [Display(Name = "Formátum")]
        public AudioFormat AlbumFormat { get; set; }
        [Display(Name = "Lemezek száma")]
        public Byte NrOfDiscs { get; set; }

        [Display(Name = "Dalok")]
        public virtual ICollection<AlbumSong> AlbumSongs { get; set; }

    }
}
