using Application.Dto.Common;

namespace Application.Dto.Books
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for detailed information about a book.
    /// </summary>
    public class BookDetailsDTO
    {
        /// <summary>
        /// Gets or sets the basic information about the book.
        /// </summary>
        public BookDTO Book { get; set; }

        /// <summary>
        /// Gets or sets the edition of the book.
        /// </summary>
        public string Edition { get; set; }

        /// <summary>
        /// Gets or sets the publisher information of the book.
        /// </summary>
        public BookPublisherDTO Publisher { get; set; }

        /// <summary>
        /// Gets or sets the publication year of the book.
        /// </summary>
        public string PublisYear { get; set; }

        /// <summary>
        /// Gets or sets the International Standard Book Number (ISBN) of the book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the collection of formats in which the book is available.
        /// </summary>
        public ICollection<BookFormatDTO> Formats { get; set; }

        /// <summary>
        /// Gets or sets the language information of the book.
        /// </summary>
        public LanguageDTO Language { get; set; }

        /// <summary>
        /// Gets or sets the collection of tags associated with the book.
        /// </summary>
        public ICollection<TagDTO> Tags { get; set; }
    }
}
