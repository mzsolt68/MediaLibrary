using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a repository interface for managing book entities.
    /// </summary>
    public interface IBookRepository : IGenericRepository<Book>
    {
        /// <summary>
        /// Retrieves a book with its full associated data, including authors, formats, and tags.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the book with its full data if found, or null otherwise.
        /// </returns>
        Task<Book?> GetBookWithFullDataAsync(Guid bookId);

        /// <summary>
        /// Deletes all authors associated with a specific book.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        void DeleteBookAuthors(Guid bookId);

        /// <summary>
        /// Deletes all formats associated with a specific book.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        void DeleteBookFormats(Guid bookId);

        /// <summary>
        /// Deletes all tags associated with a specific book.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        void DeleteBookTags(Guid bookId);
    }
}
