using Application.Abstractions.Data;
using Domain.Models.Books;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Author"/> entities.
    /// </summary>
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public AuthorRepository(MediaDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Deletes all books associated with the specified author.
        /// </summary>
        /// <param name="authorId">The unique identifier of the author whose books are to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task DeleteBooks(Guid authorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all books associated with the specified author.
        /// </summary>
        /// <param name="authorId">The unique identifier of the author whose books are to be retrieved.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing a read-only collection of books.
        /// </returns>
        public Task<IReadOnlyCollection<Book>> GetBooks(Guid authorId)
        {
            throw new NotImplementedException();
        }
    }
}
