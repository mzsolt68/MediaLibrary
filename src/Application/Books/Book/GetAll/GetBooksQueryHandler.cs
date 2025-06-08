using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using Application.Extensions;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the query to retrieve a paginated list of books based on the specified search parameters.
    /// </summary>
    /// <remarks>This query handler processes the <see cref="GetBooksQuery"/> to fetch books from the
    /// repository. It supports filtering, pagination, and includes related authors in the result.</remarks>
    /// <param name="context">The unit of work used to access the book repository and perform database operations.</param>
    public sealed class GetBooksQueryHandler(IUnitOfWork context) : IQueryHandler<GetBooksQuery, List<BookDTO>>
    {
        /// <summary>
        /// Handles the retrieval of books based on the specified query parameters.
        /// </summary>
        /// <remarks>The method supports filtering, pagination, and includes related authors for each book
        /// in the result. If no search parameters are provided, all books are retrieved with pagination
        /// applied.</remarks>
        /// <param name="request">The query containing search parameters for filtering and pagination.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="BookDTO"/> objects that match the query
        /// parameters. Returns a failure result if no books are found.</returns>
        public async Task<Result<List<BookDTO>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Book> booksQuery;
            int skip = (request.SearchParams.PageNumber - 1) * request.SearchParams.PageSize;

            if (request.SearchParams.SearchParams.Count == 0)
            {
                booksQuery = context.BookRepository.GetAll().Include(b => b.Authors);
            }
            else
            {
                booksQuery = context.BookRepository.GetAll(ExpressionBuilder.CreateFilter<Book>(request.SearchParams)).Include(b => b.Authors);
            }
            IReadOnlyList<Book> books = await booksQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);

            if (books == null || !books.Any())
            {
                return Result.Failure<List<BookDTO>>(new Error("Books.NotFound", "No books were found", ErrorType.NotFound));
            }

            var bookDtos = books.Select(book => book.AsBookDTO()).ToList();
            return Result.Success(bookDtos);
        }
    }
}
