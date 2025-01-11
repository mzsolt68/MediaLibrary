namespace Application.Dto.Audio
{
    public class AlbumDetailsDto
    {
        public AlbumDto Album { get; set; }

        public ICollection<AudioDiscDto> Discs { get; set; }
    }
}
