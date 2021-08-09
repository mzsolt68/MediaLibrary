using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Data;

namespace MediaLibrary.Repositories.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
