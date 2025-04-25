using Application.Abstractions.Data;
using Domain.Models.Books;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing book-related data operations.
    /// </summary>
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public BookRepository(MediaDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Deletes all authors associated with a specific book asynchronously.
        /// </summary>
        /// <param name="BookId">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task DeleteBookAuthorsAsync(Guid BookId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes all formats associated with a specific book asynchronously.
        /// </summary>
        /// <param name="BookId">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task DeleteBookFormatsAsync(Guid BookId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes all tags associated with a specific book asynchronously.
        /// </summary>
        /// <param name="BookId">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task DeleteBookTagsAsync(Guid BookId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a book with all its associated data (authors, formats, tags) asynchronously.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the book with its full data if found,
        /// or null otherwise.
        /// </returns>
        public Task<Book?> GetBookWithFullDataAsync(Guid bookId)
        {
            throw new NotImplementedException();
        }
    }
}
