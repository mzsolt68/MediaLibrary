using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Task DeleteBooks(Guid publisherId);
        Task<IReadOnlyCollection<Book>> GetBooks(Guid publisherId);
    }
}
