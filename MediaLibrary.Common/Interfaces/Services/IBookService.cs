using MediaLibrary.Common.Dto.Books;
using MediaLibrary.Entities.Models.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Interfaces.Services
{
    public interface IBookService
    {
        Task<BookAuthorDTO> AddAuthor(BookAuthorDTO newAuthor);
        Task<BookAuthorDTO> UpdateAuthor(BookAuthorDTO updatedAuthor);
        Task<int> DeleteAuthor(int? authorID);
        Task<BookAuthorDTO> GetAuthorByID(int? authorID);
        Task<ICollection<BookAuthorDTO>> GetAuthors();
        Task<ICollection<BookAuthorDetailsDTO>> GetBooksOfAuthor(int? authorID);

        Task<BookDetailsDTO> AddBook(BookDetailsDTO newBook);
        Task<BookDetailsDTO> UpdateBook(BookDetailsDTO updatedBook);
        Task<int> DeleteBook(int? bookID);
        Task<BookDetailsDTO> GetBookByID(int? bookID);
        Task<ICollection<BookDTO>> GetBooks();
        Task<ICollection<BookDTO>> GetBooksByTag(int? tagID);

        Task<BookFormatDTO> AddBookFormat(BookFormatDTO newFormat);
        Task<BookFormatDTO> UpdateBookFormat(BookFormatDTO updatedFormat);
        Task<int> DeleteBookFormat(int? formatID);
        Task<BookFormatDTO> GetBookFormatByID(int? formatID);
        Task<ICollection<BookFormatDTO>> GetBookFormats();
        Task<ICollection<BookDTO>> GetBooksByFormat(int? formatID);

        Task<BookPublisherDTO> AddPublisher(BookPublisherDTO newPublisher);
        Task<BookPublisherDTO> UpdatePublisher(BookPublisherDTO updatedPublisher);
        Task<int> DeletePublisher(int? publisherID);
        Task<BookPublisherDetailsDTO> GetPublisherByID(int? publisherID);
        Task<ICollection<BookPublisherDTO>> GetPublishers();
        Task<BookPublisherDetailsDTO> GetBooksOfPublisher(int? publisherID);

    }
}
