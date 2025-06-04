using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                formatsQuery = context.BookFormatRepository.GetAll(CreateFilter(request.SearchParams));
            }

            IReadOnlyList<BookFormat>? formats = await formatsQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);
            if (formats == null || !formats.Any())
            {
                return Result.Failure<List<BookFormatDTO>>(new Error("BookFormats.NotFound", "No bookformats found in the database.", ErrorType.NotFound));
            }

            var formatDtos = formats.Select(format => format.AsBookFormatDTO()).ToList();

            return Result.Success(formatDtos);

        }

        private static Expression<Func<BookFormat, bool>> CreateFilter(SearchParamsDTO searchParams)
        {
            var parameter = Expression.Parameter(typeof(BookFormat), "bookformatName");
            Expression body = Expression.Equal(
                Expression.Property(parameter, nameof(BookFormat.IsActive)),
                Expression.Constant(true)
            );

            foreach (var filter in searchParams.SearchParams)
            {
                var propertyInfo = typeof(BookFormat).GetProperty(filter.PropertyName);
                if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
                    continue;

                var property = Expression.Property(parameter, filter.PropertyName);
                var value = Expression.Constant(filter.Value, typeof(string));

                Expression filterExpr = filter.MatchType switch
                {
                    SearchType.Contains => Expression.Call(property, nameof(string.Contains), null, value),
                    SearchType.Exact => Expression.Equal(property, value),
                    SearchType.StartsWith => Expression.Call(property, nameof(string.StartsWith), null, value),
                    SearchType.EndsWith => Expression.Call(property, nameof(string.EndsWith), null, value),
                    _ => Expression.Constant(true)
                };
                body = Expression.AndAlso(body, filterExpr);
            }

            return Expression.Lambda<Func<BookFormat, bool>>(body, parameter);
        }

    }
}
