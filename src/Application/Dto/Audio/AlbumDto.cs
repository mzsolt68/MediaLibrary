using Domain.Models.Audio;

namespace Application.Dto.Audio
{
    public class AlbumDto
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public AudioFormat Format { get; set; }
        public int Nr_of_discs { get; set; }
        public int Nr_of_tracks { get; set; }
    }
}
