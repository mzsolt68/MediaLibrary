using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Models.Books;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaLibrary.Common.Dto.Books;

namespace MediaLibrary.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IAuthorRepository _authors;
        private readonly IBookFormatRepository _formats;
        private readonly IBookRepository _books;
        private readonly IPublisherRepository _publishers;

        public BookService(IAuthorRepository authors, IBookFormatRepository formats, IBookRepository books, IPublisherRepository publishers)
        {
            _authors = authors;
            _formats = formats;
            _books = books;
            _publishers = publishers;
        }

        #region Author
        public Task<BookAuthorDTO> AddAuthor(BookAuthorDTO newAuthor)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAuthor(int? authorID)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookAuthorDTO> UpdateAuthor(BookAuthorDTO updatedAuthor)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookAuthorDTO> GetAuthorByID(int? authorID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookAuthorDTO>> GetAuthors()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookAuthorDetailsDTO>> GetBooksOfAuthor(int? authorID)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Book
        public Task<BookDetailsDTO> AddBook(BookDetailsDTO newBook)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteBook(int? bookID)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookDetailsDTO> UpdateBook(BookDetailsDTO updatedBook)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookDetailsDTO> GetBookByID(int? bookID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookDTO>> GetBooks()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookDTO>> GetBooksByFormat(int? formatID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookDTO>> GetBooksByTag(int? tagID)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookPublisherDetailsDTO> GetBooksOfPublisher(int? publisherID)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Format
        public Task<BookFormatDTO> AddBookFormat(BookFormatDTO newFormat)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteBookFormat(int? formatID)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookFormatDTO> UpdateBookFormat(BookFormatDTO updatedFormat)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookFormatDTO> GetBookFormatByID(int? formatID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookFormatDTO>> GetBookFormats()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Publisher
        public Task<BookPublisherDTO> AddPublisher(BookPublisherDTO newPublisher)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeletePublisher(int? publisherID)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookPublisherDTO> UpdatePublisher(BookPublisherDTO updatedPublisher)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookPublisherDetailsDTO> GetPublisherByID(int? publisherID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookPublisherDTO>> GetPublishers()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
