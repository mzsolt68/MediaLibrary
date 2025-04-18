namespace Application.Abstractions.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository GenreRepository { get; }
        ITagRepository TagRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IBookRepository BookRepository { get; }
        IBookFormatRepository BookFormatRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
