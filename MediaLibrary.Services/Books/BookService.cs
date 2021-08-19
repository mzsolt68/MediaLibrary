using MediaLibrary.Common.Interfaces.Services;
using MediaLibrary.Common.Interfaces.Books;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaLibrary.Common.Dto.Books;
using MediaLibrary.Common;
using System.Linq;

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
        public async Task<BookAuthorDTO> AddAuthor(BookAuthorDTO newAuthor)
        {
            var result = await _authors.AddAuthor(newAuthor.AsAuthor());
            return result?.AsAuthorDTO();
        }

        public async Task<int> DeleteAuthor(int? authorID)
        {
            return await _authors.DeleteAuthor(authorID);
        }

        public async Task<BookAuthorDTO> UpdateAuthor(BookAuthorDTO updatedAuthor)
        {
            var result = await _authors.UpdateAuthor(updatedAuthor.AsAuthor());
            return result?.AsAuthorDTO();
        }

        public async Task<BookAuthorDTO> GetAuthorByID(int? authorID)
        {
            var result = await _authors.GetAuthorByID(authorID);
            return result?.AsAuthorDTO();
        }

        public async Task<ICollection<BookAuthorDTO>> GetAuthors()
        {
            List<BookAuthorDTO> result = null;
            var authors = await _authors.GetAuthors();
            if(authors.Count > 0)
            {
                result = authors.Select(a => a.AsAuthorDTO()).ToList();
            }
            return result;
        }

        public async Task<BookAuthorDetailsDTO> GetBooksOfAuthor(int? authorID)
        {
            var result = await _authors.GetBooksOfAuthor(authorID);
            return result?.AsAuthorDetailsDTO();
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
