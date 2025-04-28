namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a data transfer object for a song performer.
    /// </summary>
    public class SongPerformerDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the performer.
        /// </summary>
        public int PerformerID { get; set; }

        /// <summary>
        /// Gets or sets the name of the performer.
        /// </summary>
        public string Name { get; set; }
    }
}
