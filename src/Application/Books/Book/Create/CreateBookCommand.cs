using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to create a new book.
    /// </summary>
    public sealed record CreateBookCommand : ICommand<Guid>
    {
        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string BookTitle { get; set; }

        /// <summary>
        /// Gets or sets the edition of the book.
        /// </summary>
        public string Edition { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the publisher.
        /// </summary>
        public Guid PublisherID { get; set; }

        /// <summary>
        /// Gets or sets the year the book was published.
        /// </summary>
        public string PublishYear { get; set; }

        /// <summary>
        /// Gets or sets the International Standard Book Number (ISBN) of the book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the language of the book.
        /// </summary>
        public Guid LanguageID { get; set; }

        /// <summary>
        /// Gets or sets the collection of unique identifiers for the authors of the book.
        /// </summary>
        public ICollection<Guid> AuthorIDs { get; set; }

        /// <summary>
        /// Gets or sets the collection of unique identifiers for the formats of the book.
        /// </summary>
        public ICollection<Guid> FormatIDs { get; set; }

        /// <summary>
        /// Gets or sets the collection of unique identifiers for the tags associated with the book.
        /// </summary>
        public ICollection<Guid> TagIDs { get; set; }
    }
}