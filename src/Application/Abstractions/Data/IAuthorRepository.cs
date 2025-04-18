using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task DeleteBooks(Guid authorId);
        Task<IReadOnlyCollection<Book>> GetBooks(Guid authorId);
    }
}
