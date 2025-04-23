using Application.Abstractions.Data;
using Domain.Models.Books;

namespace Persistence.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(MediaDbContext context) : base(context)
        {
        }

        public Task DeleteBooks(Guid publisherId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Book>> GetBooks(Guid publisherId)
        {
            throw new NotImplementedException();
        }
    }
}
