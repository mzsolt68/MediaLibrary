using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Models.Audio
{
    public class PerformerSong
    {
        [Required]
        public int PerformerID { get; set; }
        public Performer Performer { get; set; }
        [Required]
        public int SongID { get; set; }
        public Song Song { get; set; }
    }
}
