using MediaLibrary.Entities.Models.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Books
{
    public interface IBookRepository
    {
        Task<Book> AddBook(Book newBook, ICollection<int> authorIDs, ICollection<int> formatIDs, ICollection<int> tagIDs);
        Task<int> DeleteBook(int? bookID);
        Task<Book> UpdateBook(Book updatedBook, ICollection<int> authorIDs, ICollection<int> formatIDs, ICollection<int> tagIDs);
        Task<ICollection<Book>> GetBooks();
        Task<Book> GetBookByID(int? bookID);
        Task<ICollection<Book>> GetBooksByFormat(int? formatID);
        Task<ICollection<Book>> GetBooksByTag(int? tagID);
        Task<Publisher> GetBooksOfPublisher(int? publisherID);
    }
}
