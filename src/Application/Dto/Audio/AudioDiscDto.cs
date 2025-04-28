namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a data transfer object for an audio disc.
    /// </summary>
    public class AudioDiscDto
    {
        /// <summary>
        /// Gets or sets the disc number.
        /// </summary>
        public int DiscNumber { get; set; }

        /// <summary>
        /// Gets or sets the collection of audio tracks on the disc.
        /// </summary>
        public ICollection<AudioTrackDto> Tracks { get; set; }
    }
}
