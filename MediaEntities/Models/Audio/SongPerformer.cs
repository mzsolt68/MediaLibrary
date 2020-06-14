using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Models.Audio
{
    public class SongPerformer
    {
        [Key]
        public int PerformerID { get; set; }
        [Required]
        [Display(Name = "Előadó neve")]
        public string PerformerName { get; set; }

        [Display(Name = "Dalok")]
        public virtual ICollection<PerformerSong> PerformerSongs { get; set; }
    }
}
