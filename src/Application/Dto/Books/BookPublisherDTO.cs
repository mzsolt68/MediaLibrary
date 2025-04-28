namespace Application.Dto.Books
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a book publisher.
    /// </summary>
    public class BookPublisherDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the publisher.
        /// </summary>
        public Guid PublisherID { get; set; }

        /// <summary>
        /// Gets or sets the name of the publisher.
        /// </summary>
        public string PublisherName { get; set; }
    }
}
