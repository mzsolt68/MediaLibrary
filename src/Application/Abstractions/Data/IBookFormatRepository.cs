using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a repository interface for managing <see cref="BookFormat"/> entities.
    /// Provides methods for performing CRUD operations and other data access logic.
    /// </summary>
    public interface IBookFormatRepository : IGenericRepository<BookFormat>
    {
    }
}
