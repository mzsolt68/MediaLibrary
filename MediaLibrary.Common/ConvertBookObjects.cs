using System;
using System.Collections.Generic;
using System.Text;
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

            };
        }

        /// <summary>
        /// Converts a BookDetails DTO to Book DB object
        /// </summary>
        /// <param name="bookDetails">BookDetails DTO</param>
        /// <returns>Book DB object</returns>
        public static Book AsBook(this BookDetailsDTO bookDetails)
        {
            return new Book()
            {

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

            };
        }
    }
}
