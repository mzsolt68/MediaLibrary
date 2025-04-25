using Application.Abstractions.Data;
using Domain.Models.Common;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Language"/> entities.
    /// </summary>
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        public LanguageRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
