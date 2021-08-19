using System.Collections.Generic;
using MediaLibrary.Entities.Models.Books;
using MediaLibrary.Common.Dto.Books;
using System.Linq;

namespace MediaLibrary.Common
{
    public static class ConvertBookObjects
    {
        /// <summary>
        /// Converts a BookAuthor DTO to Author DB object
        /// </summary>
        /// <param name="authorDTO">DTO to convert</param>
        /// <returns>DB object</returns>
        public static Author AsAuthor(this BookAuthorDTO authorDTO)
        {
            return new Author()
            {
                AuthorID = authorDTO.AuthorID,
                AuthorFirstName = authorDTO.FirstName,
                AuthorMiddleName = authorDTO.MiddleName,
                AuthorLastName = authorDTO.LastName
            };
        }

        /// <summary>
        /// Convert an Author DB object to BookAuthor DTO
        /// </summary>
        /// <param name="author">DB object to convert</param>
        /// <returns>DTO object</returns>
        public static BookAuthorDTO AsAuthorDTO(this Author author)
        {
            return new BookAuthorDTO()
            {
                AuthorID = author.AuthorID,
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
                Books = author.Books.Select(b => b.Book.AsBookDTO(false)).ToList()
            };
        }

        /// <summary>
        /// Converts a Book DB object to Book DTO
        /// </summary>
        /// <param name="book">A Book DB object</param>
        /// <param name="includeAuthors">Include Authors to DTO?</param>
        /// <returns>BookDTO object</returns>
        public static BookDTO AsBookDTO(this Book book, bool includeAuthors = true)
        {
            return new BookDTO()
            {
                BookID = book.BookID,
                BookTitle = book.BookTitle,
                Authors = !includeAuthors ? null : book.Authors.Select(a => a.Author.AsAuthorDTO()).ToList()
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
        /// Converts a BookDetails DTO to Book DB object
        /// </summary>
        /// <param name="bookDetails">BookDetails DTO</param>
        /// <returns>Book DB object</returns>
        public static Book AsBook(this BookDetailsDTO bookDetails, out ICollection<int> authors, out ICollection<int> formats, out ICollection<int> tags)
        {
            authors = bookDetails.Book.Authors.Select(a => a.AuthorID).ToList();
            formats = bookDetails.Formats.Select(f => f.FormatID).ToList();
            tags = bookDetails.Tags.Select(t => t.TagID).ToList();
            return new Book()
            {
                BookID = bookDetails.Book.BookID,
                BookTitle = bookDetails.Book.BookTitle,
                Edition = bookDetails.Edition,
                PublisherID = bookDetails.Publisher.PublisherID,
                PublishYear = bookDetails.PublisYear,
                ISBN = bookDetails.ISBN,
                LanguageID = bookDetails.Language.LanguageID
            };
        }

        /// <summary>
        /// Converts a BookFormat DTO to DB object
        /// </summary>
        /// <param name="bookFormat">A BookFormat DTO to convert</param>
        /// <returns>BookFormat DB object</returns>
        public static BookFormat AsBookFormat(this BookFormatDTO bookFormat)
        {
            return new BookFormat()
            {
                BookFormatID = bookFormat.FormatID,
                BookFormatName = bookFormat.FormatName
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
                FormatID = bookFormat.BookFormatID,
                FormatName = bookFormat.BookFormatName
            };
        }

        /// <summary>
        /// Converts a BookPublisher DTO to DB object
        /// </summary>
        /// <param name="bookPublisher">DTO to convert</param>
        /// <returns>DB object</returns>
        public static Publisher AsPublisher(this BookPublisherDTO bookPublisher)
        {
            return new Publisher()
            {
                PublisherID = bookPublisher.PublisherID,
                PublisherName = bookPublisher.PublisherName
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
                PublisherID = publisher.PublisherID,
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
    }
}
