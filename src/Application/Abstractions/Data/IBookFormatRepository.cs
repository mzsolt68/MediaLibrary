using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a repository interface for managing <see cref="BookFormat"/> entities.
    /// Provides methods for performing CRUD operations and other data access logic.
    /// </summary>
    public interface IBookFormatRepository : IGenericRepository<BookFormat>
    {
        /// <summary>
        /// Retrieves a collection of books that are available in the specified format.
        /// </summary>
        /// <remarks>The returned books include their associated authors. This method performs a database
        /// query and may incur  performance costs depending on the size of the dataset.</remarks>
        /// <param name="bookFormatId">The unique identifier of the book format to filter by.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of 
        /// <see cref="Book"/> objects that match the specified format. If no books are found, the collection will be
        /// empty.</returns>
        Task<IEnumerable<Book>> GetBooksOfFormat(Guid bookFormatId, CancellationToken cancellationToken = default);
    }
}
