namespace Application.Dto.Books
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a book.
    /// </summary>
    public class BookDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book.
        /// </summary>
        public Guid BookID { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string BookTitle { get; set; }

        /// <summary>
        /// Gets or sets the collection of authors associated with the book.
        /// </summary>
        public ICollection<BookAuthorDTO> Authors { get; set; }
    }
}
