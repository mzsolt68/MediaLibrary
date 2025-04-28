using Domain.Models.Common;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="Tag"/> entities.
    /// Provides methods for performing CRUD operations and other data access logic.
    /// </summary>
    public interface ITagRepository : IGenericRepository<Tag>
    {
    }
}
