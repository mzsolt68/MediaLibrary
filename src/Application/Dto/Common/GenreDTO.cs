namespace Application.Dto.Common
{
    /// <summary>
    /// Represents a data transfer object for a genre.
    /// </summary>
    public class GenreDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the genre.
        /// </summary>
        public Guid GenreID { get; set; }

        /// <summary>
        /// Gets or sets the name of the genre.
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Gets or sets the type of the genre.
        /// </summary>
        public string GenreType { get; set; }
    }
}
