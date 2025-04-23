using Application.Abstractions.Data;
using Domain.Models.Books;

namespace Persistence.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(MediaDbContext context) : base(context)
        {
        }

        public Task DeleteBookAuthorsAsync(Guid BookId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookFormatsAsync(Guid BookId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookTagsAsync(Guid BookId)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetBookWithFullDataAsync(Guid bookId)
        {
            throw new NotImplementedException();
        }
    }
}
