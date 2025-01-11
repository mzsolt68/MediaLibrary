namespace Application.Dto.Audio
{
    public class AudioDiscDto
    {
        public int DiscNumber { get; set; }
        public ICollection<AudioTrackDto> Tracks { get; set; }
    }
}
