namespace Application.Dto.Books
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) that contains details about a book publisher and their associated books.
    /// </summary>
    public class BookPublisherDetailsDTO
    {
        /// <summary>
        /// Gets or sets the publisher details.
        /// </summary>
        public BookPublisherDTO Publisher { get; set; }

        /// <summary>
        /// Gets or sets the collection of books associated with the publisher.
        /// </summary>
        public ICollection<BookDTO> Books { get; set; }
    }
}
