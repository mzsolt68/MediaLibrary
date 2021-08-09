using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Data;

namespace MediaLibrary.Repositories.Books
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
