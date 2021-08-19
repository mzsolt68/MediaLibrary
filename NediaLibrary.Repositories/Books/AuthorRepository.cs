using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Books;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Books
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Author> AddAuthor(Author newAuthor)
        {
            if(! await _context.Authors
                .Where(a => a.AuthorFirstName.ToLower() == newAuthor.AuthorFirstName.ToLower()
                && a.AuthorLastName.ToLower() == newAuthor.AuthorLastName.ToLower()).AnyAsync())
            {
                _context.Authors.Add(newAuthor);
                await _context.SaveChangesAsync();
                return newAuthor;
            }
            return null;
        }

        public async Task<int> DeleteAuthor(int? authorID)
        {
            int result = 0;
            var deleted = await _context.Authors.Include(ab => ab.Books).Where(a => a.AuthorID == authorID).SingleOrDefaultAsync();
            if(deleted != null)
            {
                foreach (var book in deleted.Books)
                {
                    _context.AuthorBooks.Remove(book);
                }
                await _context.SaveChangesAsync();
                _context.Authors.Remove(deleted);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Author> GetAuthorByID(int? authorID)
        {
            return await _context.Authors.Where(a => a.AuthorID == authorID).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<ICollection<Author>> GetAuthors()
        {
            return await _context.Authors.AsNoTracking().ToListAsync();
        }

        public async Task<Author> GetBooksOfAuthor(int? authorID)
        {
            var dbAuthor = await _context.Authors
                .Include(ab => ab.Books)
                .ThenInclude(b => b.Book)
                .Where(a => a.AuthorID == authorID)
                .AsNoTracking()
                .SingleOrDefaultAsync();
            return dbAuthor;
        }

        public async Task<Author> UpdateAuthor(Author updatedAuthor)
        {
            var dbAuthor = await _context.Authors.Where(a => a.AuthorID == updatedAuthor.AuthorID).SingleOrDefaultAsync();
            if(dbAuthor !=  null)
            {
                dbAuthor.AuthorFirstName = updatedAuthor.AuthorFirstName;
                dbAuthor.AuthorLastName = updatedAuthor.AuthorLastName;
                dbAuthor.AuthorMiddleName = updatedAuthor.AuthorMiddleName;
                await _context.SaveChangesAsync();
            }
            return dbAuthor;
        }
    }
}
