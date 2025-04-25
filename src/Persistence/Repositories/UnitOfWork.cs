using Application.Abstractions.Data;

namespace Persistence.Repositories
{
    /// <summary>
    /// Represents the Unit of Work pattern implementation for managing repositories and database context.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the Unit of Work.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided context is null.</exception>
        public UnitOfWork(MediaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);

        /// <inheritdoc/>
        public ITagRepository TagRepository => _tagRepository ??= new TagRepository(_context);

        /// <inheritdoc/>
        public ILanguageRepository LanguageRepository => _languageRepository ??= new LanguageRepository(_context);

        /// <inheritdoc/>
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context);

        /// <inheritdoc/>
        public IBookFormatRepository BookFormatRepository => _bookFormatRepository ??= new BookFormatRepository(_context);

        /// <inheritdoc/>
        public IAuthorRepository AuthorRepository => _authorRepository ??= new AuthorRepository(_context);

        /// <inheritdoc/>
        public IPublisherRepository PublisherRepository => _publisherRepository ??= new PublisherRepository(_context);

        /// <summary>
        /// Disposes the Unit of Work and releases the resources used by the database context.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources used by the Unit of Work.
        /// </summary>
        /// <param name="disposing">A value indicating whether to dispose managed resources.</param>
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

        /// <summary>
        /// Saves all changes made in the context to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
