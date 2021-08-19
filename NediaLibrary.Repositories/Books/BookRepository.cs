using MediaLibrary.Common.Interfaces.Books;
using MediaLibrary.Entities.Data;
using MediaLibrary.Entities.Models.Books;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Repositories.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBook(Book newBook, ICollection<int> authorIDs, ICollection<int> formatIDs, ICollection<int> tagIDs)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            foreach (var author in authorIDs)
            {
                _context.AuthorBooks.Add(
                    new AuthorBook()
                    {
                        AuthorID = author,
                        BookID = newBook.BookID
                    });
            }
            foreach (var format in formatIDs)
            {
                _context.FormatBooks.Add(
                    new FormatBook()
                    {
                        FormatID = format,
                        BookID = newBook.BookID
                    });
            }
            foreach (var tag in tagIDs)
            {
                _context.TagBooks.Add(
                    new TagBook()
                    {
                        TagID = tag,
                        BookID = newBook.BookID
                    });
            }
            await _context.SaveChangesAsync();
            return await GetBookDetails(newBook.BookID);
        }

        public async Task<int> DeleteBook(int? bookID)
        {
            var dbResult = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Formats)
                .Include(b => b.Tags)
                .Where(b => b.BookID == bookID).SingleOrDefaultAsync();
            if(dbResult != null)
            {
                foreach(var author in dbResult.Authors)
                {
                    _context.AuthorBooks.Remove(author);
                }
                foreach (var format in dbResult.Formats)
                {
                    _context.FormatBooks.Remove(format);
                }
                foreach (var tag in dbResult.Tags)
                {
                    _context.TagBooks.Remove(tag);
                }
                _context.Books.Remove(dbResult);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Book> GetBookByID(int? bookID)
        {
            return await GetBookDetails(bookID);
        }

        public async Task<ICollection<Book>> GetBooks()
        {
            return await _context.Books
                .Include(b => b.Authors)
                .ThenInclude(a => a.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<Book>> GetBooksByFormat(int? formatID)
        {
            return await _context.FormatBooks
                .Include(f => f.Book)
                .ThenInclude(b => b.Authors)
                .ThenInclude(a => a.Author)
                .Where(f => f.FormatID == formatID)
                .Select(fb => fb.Book)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<Book>> GetBooksByTag(int? tagID)
        {
            return await _context.TagBooks
                .Include(t => t.Book)
                .ThenInclude(b => b.Authors)
                .ThenInclude(a => a.Author)
                .Where(t => t.TagID == tagID)
                .Select(tb => tb.Book)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Publisher> GetBooksOfPublisher(int? publisherID)
        {
            return await _context.Publishers
                .Include(p => p.PublishedBooks)
                .ThenInclude(pb => pb.Authors)
                .ThenInclude(a => a.Author)
                .AsNoTracking()
                .Where(p => p.PublisherID == publisherID).SingleOrDefaultAsync();
        }

        public async Task<Book> UpdateBook(Book updatedBook, ICollection<int> authorIDs, ICollection<int> formatIDs, ICollection<int> tagIDs)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.BookID == updatedBook.BookID);
            if(book != null)
            {
                await UpdateAuthors(updatedBook.BookID, authorIDs);
                await UpdateFormats(updatedBook.BookID, formatIDs);
                await UpdateTags(updatedBook.BookID, tagIDs);
                await _context.SaveChangesAsync();
                book.BookTitle = updatedBook.BookTitle;
                book.Edition = updatedBook.Edition;
                book.PublisherID = updatedBook.PublisherID;
                book.PublishYear = updatedBook.PublishYear;
                book.ISBN = updatedBook.ISBN;
                book.LanguageID = updatedBook.LanguageID;
                await _context.SaveChangesAsync();
                book = await GetBookDetails(updatedBook.BookID);
            }
            return book;
        }

        private async Task<Book> GetBookDetails(int? bookID)
        {
            return await _context.Books
                .Include(b => b.Authors)
                .ThenInclude(a => a.Author)
                .Include(b => b.Formats)
                .ThenInclude(f => f.Format)
                .Include(b => b.Tags)
                .ThenInclude(t => t.Tag)
                .AsNoTracking()
                .Where(b => b.BookID == bookID)
                .SingleOrDefaultAsync();
        }

        private async Task UpdateAuthors(int bookID, ICollection<int> authorIDs)
        {
            var authorsInDb = await _context.AuthorBooks.Where(ab => ab.BookID == bookID).AsNoTracking().ToListAsync();
            foreach (var author in authorsInDb)
            {
                if (!authorIDs.Contains(author.AuthorID))
                {
                    _context.AuthorBooks.Remove(author);
                }
            }
            foreach (var author in authorIDs)
            {
                if (!authorsInDb.Select(a => a.AuthorID).ToList().Contains(author))
                {
                    _context.AuthorBooks.Add(
                        new AuthorBook()
                        {
                            AuthorID = author,
                            BookID = bookID
                        });
                }
            }
        }

        private async Task UpdateFormats(int bookID, ICollection<int> formatIDs)
        {
            var formatsInDb = await _context.FormatBooks.Where(fb => fb.BookID == bookID).AsNoTracking().ToListAsync();
            foreach (var format in formatsInDb)
            {
                if (!formatIDs.Contains(format.FormatID))
                {
                    _context.FormatBooks.Remove(format);
                }
            }
            foreach (var format in formatIDs)
            {
                if (!formatsInDb.Select(f => f.FormatID).ToList().Contains(format))
                {
                    _context.FormatBooks.Add(
                        new FormatBook()
                        {
                            FormatID = format,
                            BookID = bookID
                        });
                }
            }
        }

        private async Task UpdateTags(int bookID, ICollection<int> tagIDs)
        {
            var tagsInDb = await _context.TagBooks.Where(tb => tb.BookID == bookID).AsNoTracking().ToListAsync();
            foreach (var tag in tagsInDb)
            {
                if (!tagIDs.Contains(tag.TagID))
                {
                    _context.TagBooks.Remove(tag);
                }
            }
            foreach (var tag in tagIDs)
            {
                if (!tagsInDb.Select(t => t.TagID).ToList().Contains(tag))
                {
                    _context.TagBooks.Add(
                        new TagBook()
                        {
                            TagID = tag,
                            BookID = bookID
                        });
                }
            }
        }
    }
}
