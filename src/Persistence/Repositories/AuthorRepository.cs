using Application.Abstractions.Data;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Author"/> entities.
    /// </summary>
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly MediaDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public AuthorRepository(MediaDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes all books associated with the specified author.
        /// </summary>
        /// <param name="authorId">The unique identifier of the author whose books are to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteBooks(Guid authorId)
        {
            var authorBooks = _context.AuthorsOfBooks
                .Where(ab => ab.AuthorID == authorId);

            _context.AuthorsOfBooks.RemoveRange(authorBooks);
        }

        /// <summary>
        /// Retrieves all books associated with the specified author.
        /// </summary>
        /// <param name="authorId">The unique identifier of the author whose books are to be retrieved.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing a read-only collection of books.
        /// </returns>
        public async Task<IReadOnlyCollection<Book>> GetBooks(Guid authorId)
        {
            return await _context.AuthorsOfBooks
                .Where(ab => ab.AuthorID == authorId)
                .Select(ab => ab.Book)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
