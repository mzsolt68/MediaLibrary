namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a data transfer object containing details about a performer and their associated songs.
    /// </summary>
    public class PerformerDetailsDto
    {
        /// <summary>
        /// Gets or sets the performer details.
        /// </summary>
        public SongPerformerDto Performer { get; set; }

        /// <summary>
        /// Gets or sets the collection of songs associated with the performer.
        /// </summary>
        public ICollection<SongDto> Songs { get; set; }
    }
}
