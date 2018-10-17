using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audio
{
    public class Performer
    {
        public int PerformerID { get; set; }
        [Required]
        [Display(Name = "Előadó neve")]
        public string PerformerName { get; set; }

        public virtual ICollection<PerformerSong> PerformerSongs { get; set; }
    }
}
