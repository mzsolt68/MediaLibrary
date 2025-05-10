using Application.Dto.Books;
using Domain.Models.Books;

namespace Application.Dto.ConvertObjects
{
    public static class ConvertBookObjects
    {
        /// <summary>
        /// Convert an Author DB object to BookAuthor DTO
        /// </summary>
        /// <param name="author">DB object to convert</param>
        /// <returns>DTO object</returns>
        public static BookAuthorDTO AsAuthorDTO(this Author author)
        {
            return new BookAuthorDTO()
            {
                AuthorID = author.Id,
                FirstName = author.AuthorFirstName,
                MiddleName = author.AuthorMiddleName,
                LastName = author.AuthorLastName
            };
        }

        /// <summary>
        /// Convert an Author DB object with book included to BookAuthorDetails DTO
        /// </summary>
        /// <param name="author">Author DB object with books included</param>
        /// <returns>AuthorDetails DTO</returns>
        public static BookAuthorDetailsDTO AsAuthorDetailsDTO(this Author author)
        {
            return new BookAuthorDetailsDTO()
            {
                Author = author.AsAuthorDTO(),
                Books = author.Books.Select(b => b.AsBookDTO(false)).ToList()
            };
        }

        /// <summary>
        /// Converts a Book DB object to BookDetails DTO
        /// </summary>
        /// <param name="book">Book DB object</param>
        /// <returns>BookDetails DTO</returns>
        public static BookDetailsDTO AsBookDetailsDTO(this Book book)
        {
            return new BookDetailsDTO()
            {
                Book = book.AsBookDTO(),
                Edition = book.Edition,
                ISBN = book.ISBN,
                PublisYear = book.PublishYear,
                Language = book.Language.AsLanguageDTO(),
                Publisher = book.Publisher.AsPublisherDTO(),
                Formats = book.Formats.Select(f => f.Format.AsBookFormatDTO()).ToList(),
                Tags = book.Tags.Select(t => t.Tag.AsTagDTO()).ToList()
            };
        }

        /// <summary>
        /// Converts a BookFormat DB object to DTO
        /// </summary>
        /// <param name="bookFormat">DB object to convert</param>
        /// <returns>BookFormat DTO</returns>
        public static BookFormatDTO AsBookFormatDTO(this BookFormat bookFormat)
        {
            return new BookFormatDTO()
            {
                FormatID = bookFormat.Id,
                FormatName = bookFormat.BookFormatName
            };
        }

        /// <summary>
        /// Converts a Publisher DB object to BookPublisher DTO
        /// </summary>
        /// <param name="publisher">DB object to convert</param>
        /// <returns>DTO object</returns>
        public static BookPublisherDTO AsPublisherDTO(this Publisher publisher)
        {
            return new BookPublisherDTO()
            {
                PublisherID = publisher.Id,
                PublisherName = publisher.PublisherName
            };
        }

        /// <summary>
        /// Converts a Publisher DB object to BookPublisherDetails DTO
        /// </summary>
        /// <param name="publisher">DB object to convert</param>
        /// <returns>DTO object</returns>
        public static BookPublisherDetailsDTO AsPublisherDetailsDTO(this Publisher publisher)
        {
            return new BookPublisherDetailsDTO()
            {
                Publisher = publisher.AsPublisherDTO(),
                Books = publisher.PublishedBooks.Select(p => p.AsBookDTO()).ToList()
            };
        }

        public static BookDTO AsBookDTO(this Book book, bool includeAuthor = true)
        {
            return new BookDTO()
            {
                BookID = book.Id,
                BookTitle = book.BookTitle,
                Authors = includeAuthor ? book.Authors.Select(a => a.Author.AsAuthorDTO()).ToList() : []
            };
        }
    }
}
