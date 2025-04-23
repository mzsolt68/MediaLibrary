using Application.Abstractions.Data;
using Domain.Models.Books;

namespace Persistence.Repositories
{
    public class BookFormatRepository : GenericRepository<BookFormat>, IBookFormatRepository
    {
        public BookFormatRepository(MediaDbContext context) : base(context)
        {
        }
    }
}
