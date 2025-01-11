using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Audio
{
    public class SongDetailsDto
    {
        public SongDto Song { get; set; }
        public string Liryc { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }

        [Display(Name = "Album(ok)")]
        public ICollection<AlbumSongDto> Albums { get; set; }
    }
}
