using MediaLibrary.Entities.Models.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Services
{
    public interface IBookService
    {
        Task<Author> AddAuthor(Author newAuthor);
        Task<Author> UpdateAuthor(Author updatedAuthor);
        Task<int> DeleteAuthor(int? authorID);
        Task<Author> GetAuthorByID(int? authorID);
        Task<ICollection<Author>> GetAuthors();
        Task<ICollection<Book>> GetBooksOfAuthor(int? authorID);

        Task<Book> AddBook(Book newBook);
        Task<Book> UpdateBook(Book updatedBook);
        Task<int> DeleteBook(int? bookID);
        Task<Book> GetBookByID(int? bookID);
        Task<ICollection<Book>> GetBooks();
        Task<ICollection<Book>> GetBooksByTag(int? tagID);

        Task<BookFormat> AddBookFormat(BookFormat newFormat);
        Task<BookFormat> UpdateBookFormat(BookFormat updatedFormat);
        Task<int> DeleteBookFormat(int? formatID);
        Task<BookFormat> GetBookFormatByID(int? formatID);
        Task<ICollection<BookFormat>> GetBookFormats();
        Task<ICollection<Book>> GetBooksByFormat(int? formatID);

        Task<Publisher> AddPublisher(Publisher newPublisher);
        Task<Publisher> UpdatePublisher(Publisher updatedPublisher);
        Task<int> DeletePublisher(int? publisherID);
        Task<Publisher> GetPublisherByID(int? publisherID);
        Task<ICollection<Publisher>> GetPublishers();
        Task<ICollection<Book>> GetBooksOfPublisher(int? publisherID);

    }
}
