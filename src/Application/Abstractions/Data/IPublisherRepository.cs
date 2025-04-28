using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a repository interface for managing publishers and their associated books.
    /// </summary>
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        /// <summary>
        /// Deletes all books associated with the specified publisher.
        /// </summary>
        /// <param name="publisherId">The unique identifier of the publisher whose books are to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteBooks(Guid publisherId);

        /// <summary>
        /// Retrieves all books associated with the specified publisher.
        /// </summary>
        /// <param name="publisherId">The unique identifier of the publisher whose books are to be retrieved.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of books.
        /// </returns>
        Task<IReadOnlyCollection<Book>> GetBooks(Guid publisherId);
    }
}
