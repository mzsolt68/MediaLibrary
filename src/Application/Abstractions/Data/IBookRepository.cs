using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetBookWithFullDataAsync(Guid bookId);
        Task DeleteAuthorsAsync(Guid BookId);
        Task DeleteFormatsAsync(Guid BookId);
        Task DeleteTagsAsync(Guid BookId);
    }
}
