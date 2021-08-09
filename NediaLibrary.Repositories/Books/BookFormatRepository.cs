using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Data;

namespace MediaLibrary.Repositories.Books
{
    public class BookFormatRepository : IBookFormatRepository
    {
        private readonly ApplicationDbContext _context;

        public BookFormatRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
