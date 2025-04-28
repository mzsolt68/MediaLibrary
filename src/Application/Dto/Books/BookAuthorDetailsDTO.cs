namespace Application.Dto.Books
{
    /// <summary>
    /// Represents the details of a book author, including the author information and their associated books.
    /// </summary>
    public class BookAuthorDetailsDTO
    {
        /// <summary>
        /// Gets or sets the author information.
        /// </summary>
        public BookAuthorDTO Author { get; set; }

        /// <summary>
        /// Gets or sets the collection of books associated with the author.
        /// </summary>
        public ICollection<BookDTO> Books { get; set; }
    }
}
