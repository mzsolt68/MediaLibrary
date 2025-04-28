using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a repository interface for managing authors and their associated books.
    /// </summary>
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        /// <summary>
        /// Deletes all books associated with the specified author.
        /// </summary>
        /// <param name="authorId">The unique identifier of the author whose books are to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteBooks(Guid authorId);

        /// <summary>
        /// Retrieves all books associated with the specified author.
        /// </summary>
        /// <param name="authorId">The unique identifier of the author whose books are to be retrieved.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a read-only collection of books associated with the author.
        /// </returns>
        Task<IReadOnlyCollection<Book>> GetBooks(Guid authorId);
    }
}
