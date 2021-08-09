using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Data;

namespace MediaLibrary.Repositories.Books
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDbContext _context;

        public PublisherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
