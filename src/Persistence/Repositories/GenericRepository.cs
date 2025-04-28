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
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// Deletes an existing entity from the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>  
        /// Retrieves all entities from the database asynchronously based on a predicate.  
        /// </summary>  
        /// <param name="predicate">A predicate to filter the entities.</param>  
        /// <returns>A task representing the asynchronous operation, containing a read-only list of entities.</returns>  
        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task representing the asynchronous operation, containing the entity if found, or null otherwise.</returns>
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id);
        }

        /// <summary>
        /// Updates an existing entity in the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
