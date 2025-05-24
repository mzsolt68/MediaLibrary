using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Linq.Expressions;

namespace Application.Books
{
    /// <summary>
    /// Handles the query to retrieve a paginated and filtered list of authors from the database.
    /// </summary>
    /// <remarks>This query handler processes the <see cref="GetAuthorsQuery"/> to fetch authors based on the
    /// provided search parameters, including pagination and optional filtering criteria. If no filters are specified,
    /// all authors are retrieved. The result is returned as a list of <see cref="BookAuthorDTO"/> objects.</remarks>
    /// <param name="context"></param>
    public sealed class GetAuthorsQueryHandler(IUnitOfWork context) : IQueryHandler<GetAuthorsQuery, List<BookAuthorDTO>>
    {
        /// <summary>
        /// Handles the retrieval of authors based on the specified query parameters.
        /// </summary>
        /// <remarks>This method retrieves authors from the database based on the provided search
        /// parameters. If no search parameters are specified, all authors are retrieved. The results are paginated
        /// based on the page number and page size provided in the query.</remarks>
        /// <param name="request">The query containing search parameters for filtering and pagination.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="BookAuthorDTO"/> objects if authors are found;
        /// otherwise, a failure result with an appropriate error message.</returns>
        public async Task<Result<List<BookAuthorDTO>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Author> authorsQuery;
            int skip = (request.SearchParams.PageNumber - 1) * request.SearchParams.PageSize;

            if (request.SearchParams.SearchParams.Count == 0)
            {
                authorsQuery = context.AuthorRepository.GetAll();
            }
            else
            {
                authorsQuery = context.AuthorRepository.GetAll(CreateFilter(request.SearchParams));
            }

            IReadOnlyList<Author>? authors = await authorsQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);
            // Check if genres are null or empty and return a failure result if so.
            if (authors == null || !authors.Any())
            {
                return Result.Failure<List<BookAuthorDTO>>(new Error("Authors.NotFound", "No authors found in the database.", ErrorType.NotFound));
            }

            // Map the genres to their DTO representations.
            var authorDtos = authors.Select(auhtor => auhtor.AsAuthorDTO()).ToList();

            // Return a success result with the list of genre DTOs.
            return Result.Success(authorDtos);

        }

        private static Expression<Func<Author, bool>> CreateFilter(SearchParamsDTO searchParams)
        {
            var parameter = Expression.Parameter(typeof(Author), "genre");
            Expression body = Expression.Equal(
                Expression.Property(parameter, nameof(Author.IsActive)),
                Expression.Constant(true)
            );

            foreach (var filter in searchParams.SearchParams)
            {
                var propertyInfo = typeof(Author).GetProperty(filter.PropertyName);
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

            return Expression.Lambda<Func<Author, bool>>(body, parameter);
        }

    }
}
