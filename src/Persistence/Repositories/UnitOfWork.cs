using Application.Abstractions.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MediaDbContext _context;
        private bool _disposed;

        public UnitOfWork(MediaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IGenreRepository GenreRepository => throw new NotImplementedException();

        public ITagRepository TagRepository => throw new NotImplementedException();

        public ILanguageRepository LanguageRepository => throw new NotImplementedException();

        public IBookRepository BookRepository => throw new NotImplementedException();

        public IBookFormatRepository BookFormatRepository => throw new NotImplementedException();

        public IAuthorRepository AuthorRepository => throw new NotImplementedException();

        public IPublisherRepository PublisherRepository => throw new NotImplementedException();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
