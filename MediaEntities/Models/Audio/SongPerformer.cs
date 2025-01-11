using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Audio
{
    public class SongPerformer
    {
        [Key]
        public int PerformerID { get; set; }
        [Required]
        [Display(Name = "Előadó neve")]
        public string PerformerName { get; set; }

        [Display(Name = "Dalok")]
        public virtual ICollection<PerformerSong> Songs { get; set; }
    }
}
