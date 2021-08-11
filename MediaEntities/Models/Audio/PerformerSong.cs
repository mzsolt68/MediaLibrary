using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Entities.Models.Audio
{
    public class PerformerSong
    {
        [Required]
        public int PerformerID { get; set; }
        public SongPerformer Performer { get; set; }
        [Required]
        public int SongID { get; set; }
        public Song Song { get; set; }
    }
}
