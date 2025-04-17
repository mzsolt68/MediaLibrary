namespace Application.Abstractions.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository GenreRepository { get; }
        ITagRepository TagRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
