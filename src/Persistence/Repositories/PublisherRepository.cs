using Application.Abstractions.Data;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Publisher"/> entities.
    /// </summary>
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private readonly MediaDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to use for data operations.</param>
        public PublisherRepository(MediaDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes all books associated with the specified publisher.
        /// </summary>
        /// <param name="publisherId">The unique identifier of the publisher whose books are to be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public void DeleteBooks(IEnumerable<Book> books)
        {
            if (books.Any())
            {
                _context.Books.RemoveRange(books);
            }
        }

        /// <summary>
        /// Retrieves all books associated with the specified publisher.
        /// </summary>
        /// <param name="publisherId">The unique identifier of the publisher whose books are to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation, 
        /// with a result of a read-only collection of <see cref="Book"/> entities.
        /// </returns>
        public async Task<IReadOnlyCollection<Book>> GetPublishersBooksAsync(Guid publisherId)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(book => book.PublisherID == publisherId)
                .ToListAsync();
        }
    }
}
