namespace Application.Dto.Audio
{
    public class AudioTrackDto
    {
        public int SongID { get; set; }
        public int TrackNr { get; set; }
        public ICollection<string> Performer { get; set; }
        public string Title { get; set; }
        public string PlayTime { get; set; }
        public string Note { get; set; }

    }
}
