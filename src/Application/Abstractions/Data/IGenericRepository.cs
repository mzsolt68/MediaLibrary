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
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found, or null otherwise.</returns>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <param name="predicate">A predicate to filter the entities.</param>
        /// <returns>A query object with filter for further processing.</returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A query object for further processing.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Determines whether any entities in the data source satisfy the specified condition.
        /// </summary>
        /// <param name="predicate">An expression that defines the condition to test against the entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains  <see langword="true"/> if any
        /// entities satisfy the condition specified by <paramref name="predicate"/>;  otherwise, <see
        /// langword="false"/>.</returns>
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
