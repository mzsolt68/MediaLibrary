using Domain.Models.Audio;

namespace Application.Dto.Audio
{
    public class AlbumSongDto
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public AudioFormat Format { get; set; }
        public string TrackNr { get; set; }
        public string PlayTime { get; set; }
    }
}
