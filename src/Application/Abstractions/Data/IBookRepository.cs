using Domain.Models.Books;

namespace Application.Abstractions.Data
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetBookWithFullDataAsync(Guid bookId);
        Task DeleteBookAuthorsAsync(Guid BookId);
        Task DeleteBookFormatsAsync(Guid BookId);
        Task DeleteBookTagsAsync(Guid BookId);
    }
}
