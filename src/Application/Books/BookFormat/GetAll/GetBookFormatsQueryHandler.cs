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
    /// Handles queries to retrieve a paginated list of book formats based on search parameters.
    /// </summary>
    /// <remarks>This query handler processes requests to fetch book formats from the database, applying
    /// optional search filters and pagination. If no search parameters are provided, all active book formats are
    /// retrieved. The result is returned as a list of <see cref="BookFormatDTO"/> objects.</remarks>
    /// <param name="context">The unit of work providing access to the repository for querying book formats.</param>
    public sealed class GetBookFormatsQueryHandler(IUnitOfWork context) : IQueryHandler<GetBookFormatsQuery, List<BookFormatDTO>>
    {
        /// <summary>
        /// Handles the retrieval of book formats based on the specified query parameters.
        /// </summary>
        /// <remarks>The method supports filtering and pagination based on the provided search parameters.
        /// If no filters are specified, all book formats are retrieved. The result is paginated based on the page
        /// number and page size in the query.</remarks>
        /// <param name="request">The query containing search parameters for filtering and pagination.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="BookFormatDTO"/> objects that match the query
        /// parameters. Returns a failure result if no book formats are found.</returns>
        public async Task<Result<List<BookFormatDTO>>> Handle(GetBookFormatsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<BookFormat> formatsQuery;
            int skip = (request.SearchParams.PageNumber - 1) * request.SearchParams.PageSize;

            if (request.SearchParams.SearchParams.Count == 0)
            {
                formatsQuery = context.BookFormatRepository.GetAll();
            }
            else
            {
                formatsQuery = context.BookFormatRepository.GetAll(ExpressionBuilder.CreateFilter<BookFormat>(request.SearchParams));
            }

            IReadOnlyList<BookFormat>? formats = await formatsQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);
            if (formats == null || !formats.Any())
            {
                return Result.Failure<List<BookFormatDTO>>(new Error("BookFormats.NotFound", "No bookformats found in the database.", ErrorType.NotFound));
            }

            var formatDtos = formats.Select(format => format.AsBookFormatDTO()).ToList();

            return Result.Success(formatDtos);
        }
    }
}
