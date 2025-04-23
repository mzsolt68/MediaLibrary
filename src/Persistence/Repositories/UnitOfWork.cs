using Application.Abstractions.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MediaDbContext _context;
        private IGenreRepository? _genreRepository;
        private ITagRepository? _tagRepository;
        private ILanguageRepository? _languageRepository;
        private IBookRepository? _bookRepository;
        private IBookFormatRepository? _bookFormatRepository;
        private IAuthorRepository? _authorRepository;
        private IPublisherRepository? _publisherRepository;
        private bool _disposed;

        public UnitOfWork(MediaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);

        public ITagRepository TagRepository => _tagRepository ??= new TagRepository(_context);

        public ILanguageRepository LanguageRepository => _languageRepository ??= new LanguageRepository(_context);

        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context);

        public IBookFormatRepository BookFormatRepository => _bookFormatRepository ??= new BookFormatRepository(_context);

        public IAuthorRepository AuthorRepository => _authorRepository ??= new AuthorRepository(_context);

        public IPublisherRepository PublisherRepository => _publisherRepository ??= new PublisherRepository(_context);

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
