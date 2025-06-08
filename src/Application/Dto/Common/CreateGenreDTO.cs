namespace Application.Dto.Common
{
    /// <summary>
    /// Represents the data transfer object used to create a new genre.
    /// </summary>
    /// <remarks>This class is typically used to encapsulate the information required to define a genre,
    /// including its name and type, when creating a new genre in the system.</remarks>
    public class CreateGenreDTO
    {
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
