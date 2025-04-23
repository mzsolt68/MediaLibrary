using Application.Abstractions.Data;
using Domain.Models.Books;

namespace Persistence.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(MediaDbContext context) : base(context)
        {
        }
        public Task DeleteBooks(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Book>> GetBooks(Guid authorId)
        {
            throw new NotImplementedException();
        }
    }
}
