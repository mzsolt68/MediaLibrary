using Application.Abstractions.Data;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="BookFormat"/> entities.
    /// </summary>
    public class BookFormatRepository : GenericRepository<BookFormat>, IBookFormatRepository
    {
        private readonly MediaDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="BookFormatRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public BookFormatRepository(MediaDbContext context) : base(context)
        {
            _context = context;
        }

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
        public async Task<IEnumerable<Book>> GetBooksOfFormat(Guid bookFormatId, CancellationToken cancellationToken)
        {
            return await _context.Books
                .Include(b => b.Authors)
                .Where(b => b.Formats.Any(f => f.Id == bookFormatId))
                .ToListAsync();
        }
    }
}
