using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Models.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public Task<Author> AddAuthor(Author newAuthor)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAuthor(int? authorID)
        {
            throw new System.NotImplementedException();
        }

        public Task<Author> UpdateAuthor(Author updatedAuthor)
        {
            throw new System.NotImplementedException();
        }

        public Task<Author> GetAuthorByID(int? authorID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Author>> GetAuthors()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Book>> GetBooksOfAuthor(int? authorID)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Book
        public Task<Book> AddBook(Book newBook)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteBook(int? bookID)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> UpdateBook(Book updatedBook)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> GetBookByID(int? bookID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Book>> GetBooks()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Book>> GetBooksByFormat(int? formatID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Book>> GetBooksByTag(int? tagID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Book>> GetBooksOfPublisher(int? publisherID)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Format
        public Task<BookFormat> AddBookFormat(BookFormat newFormat)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteBookFormat(int? formatID)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookFormat> UpdateBookFormat(BookFormat updatedFormat)
        {
            throw new System.NotImplementedException();
        }

        public Task<BookFormat> GetBookFormatByID(int? formatID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BookFormat>> GetBookFormats()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Publisher
        public Task<Publisher> AddPublisher(Publisher newPublisher)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeletePublisher(int? publisherID)
        {
            throw new System.NotImplementedException();
        }

        public Task<Publisher> UpdatePublisher(Publisher updatedPublisher)
        {
            throw new System.NotImplementedException();
        }

        public Task<Publisher> GetPublisherByID(int? publisherID)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Publisher>> GetPublishers()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
