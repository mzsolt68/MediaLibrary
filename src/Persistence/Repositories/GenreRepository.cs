using Application.Abstractions.Data;
using Domain.Models.Common;

namespace Persistence.Repositories
{
    // Implement the IGenreRepository interface
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
