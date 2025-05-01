using System.Linq.Expressions;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents a generic repository interface for performing CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found, or null otherwise.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <param name="predicate">A predicate to filter the entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of entities.</returns>
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity);
    }
}
