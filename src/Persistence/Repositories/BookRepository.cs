using Application.Abstractions.Data;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    /// <summary>
    /// Repository for managing book-related data operations.
    /// </summary>
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly MediaDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public BookRepository(MediaDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes all authors associated with a specific book asynchronously.
        /// </summary>
        /// <param name="BookId">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public void DeleteBookAuthors(Guid BookId)
        {
            var authorsToRemove = _context.AuthorsOfBooks.Where(ab => ab.BookID == BookId);
            _context.AuthorsOfBooks.RemoveRange(authorsToRemove);
        }

        /// <summary>
        /// Deletes all formats associated with a specific book asynchronously.
        /// </summary>
        /// <param name="BookId">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public void DeleteBookFormats(Guid BookId)
        {
            var formatsToRemove = _context.FormatsOfBooks.Where(fb => fb.BookID == BookId);
            _context.FormatsOfBooks.RemoveRange(formatsToRemove);
        }

        /// <summary>
        /// Deletes all tags associated with a specific book asynchronously.
        /// </summary>
        /// <param name="BookId">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public void DeleteBookTags(Guid BookId)
        {
            var tagsToRemove = _context.TagsOfBooks.Where(tb => tb.BookID == BookId);
            _context.TagsOfBooks.RemoveRange(tagsToRemove);
        }

        /// <summary>
        /// Retrieves a book with all its associated data (authors, formats, tags) asynchronously.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the book with its full data if found,
        /// or null otherwise.
        /// </returns>
        public async Task<Book?> GetBookWithFullDataAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Authors)
                .Include(b => b.Formats)
                .Include(b => b.Tags)
                .Include(b => b.Language)
                .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken: cancellationToken);
        }

        public new async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            return await _context.Books
                .Include(b => b.Authors)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellation);
        }
    }
}
