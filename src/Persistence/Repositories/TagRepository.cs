using Application.Abstractions.Data;
using Domain.Models.Common;

namespace Persistence.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
