using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    /// <summary>
    /// A generic repository implementation for performing CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MediaDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public GenericRepository(MediaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new entity to the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation, containing the added entity.</returns>
        public void Add(T entity)
        {
            _context.AddAsync(entity);
        }

        /// <summary>
        /// Deletes an existing entity from the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>  
        /// Retrieves all entities from the database asynchronously based on a predicate.  
        /// </summary>  
        /// <param name="predicate">A predicate to filter the entities.</param>  
        /// <returns>A query object with filter for further processing.</returns>  
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().Where(predicate);
        }

        /// <summary>  
        /// Retrieves all entities from the database asynchronously based on a predicate.  
        /// </summary>  
        /// <returns>A query object for further processing.</returns>  
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }


        /// <summary>
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="cancellationToken"> A cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, containing the entity if found, or null otherwise.</returns>
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing entity in the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Determines whether any entities in the database match the specified condition.
        /// </summary>
        /// <remarks>This method asynchronously evaluates the condition against the entities in the
        /// database. It is useful for checking the existence of records without retrieving them.</remarks>
        /// <param name="predicate">An expression that defines the condition to test against the entities.</param>
        /// <returns><see langword="true"/> if any entities satisfy the specified condition; otherwise, <see langword="false"/>.</returns>
        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
    }
}
