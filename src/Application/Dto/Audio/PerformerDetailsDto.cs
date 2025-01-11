namespace Application.Dto.Audio
{
    public class PerformerDetailsDto
    {
        public SongPerformerDto Performer { get; set; }
        public ICollection<SongDto> Songs { get; set; }
    }
}
