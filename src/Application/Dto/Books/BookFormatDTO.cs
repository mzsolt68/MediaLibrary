namespace Application.Dto.Books
{
    /// <summary>
    /// Represents the data transfer object for a book format.
    /// </summary>
    public class BookFormatDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book format.
        /// </summary>
        public Guid FormatID { get; set; }

        /// <summary>
        /// Gets or sets the name of the book format.
        /// </summary>
        public string FormatName { get; set; }
    }
}
