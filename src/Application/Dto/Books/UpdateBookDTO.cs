namespace Application.Dto.Books
{
    public class UpdateBookDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book to be updated.
        /// </summary>
        public Guid BookID { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string BookTitle { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the edition of the book.
        /// </summary>
        public string Edition { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the publisher.
        /// </summary>
        public Guid PublisherID { get; set; }

        /// <summary>
        /// Gets or sets the year the book was published.
        /// </summary>
        public string PublishYear { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the International Standard Book Number (ISBN) of the book.
        /// </summary>
        public string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the language the book is written in.
        /// </summary>
        public Guid LanguageID { get; set; }

        /// <summary>
        /// Gets or sets the collection of unique identifiers for the authors of the book.
        /// </summary>
        public ICollection<Guid>? AuthorIDs { get; set; }

        /// <summary>
        /// Gets or sets the collection of unique identifiers for the formats of the book.
        /// </summary>
        public ICollection<Guid>? FormatIDs { get; set; }

        /// <summary>
        /// Gets or sets the collection of unique identifiers for the tags associated with the book.
        /// </summary>
        public ICollection<Guid>? TagIDs { get; set; }
    }
}
