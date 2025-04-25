using Application.Abstractions.Data;
using Domain.Models.Common;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Genre"/> entities.
    /// </summary>
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenreRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        public GenreRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
