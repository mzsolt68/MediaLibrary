using Application.Abstractions.Data;
using Domain.Models.Common;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Tag"/> entities.
    /// </summary>
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        public TagRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
