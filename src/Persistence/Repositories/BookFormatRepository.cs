using Application.Abstractions.Data;
using Domain.Models.Books;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="BookFormat"/> entities.
    /// </summary>
    public class BookFormatRepository : GenericRepository<BookFormat>, IBookFormatRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookFormatRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public BookFormatRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
