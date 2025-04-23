using Application.Abstractions.Data;
using Domain.Models.Common;

namespace Persistence.Repositories
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
