using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;
using Domain.Models.Common;
using System.Linq.Expressions;
using Application.Dto;
using SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve all genres.
    /// </summary>
    /// <param name="context">The unit of work providing access to repositories.</param>
    public sealed class GetGenresQueryHandler(IUnitOfWork context) : IQueryHandler<GetGenresQuery, List<GenreDTO>>
    {
        /// <summary>
        /// Handles the query to retrieve all genres.
        /// </summary>
        /// <param name="request">The query request containing any necessary parameters.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// with a list of <see cref="GenreDTO"/> if successful, or an error if no genres are found.
        /// </returns>
        public async Task<Result<List<GenreDTO>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Genre> genresQuery;
            int skip = (request.SearchParams.PageNumber - 1) * request.SearchParams.PageSize;

            if (request.SearchParams.SearchParams.Count == 0)
            {
                genresQuery = context.GenreRepository.GetAll();
            }
            else
            {
                genresQuery = context.GenreRepository.GetAll(CreateFilter(request.SearchParams));
            }

            IReadOnlyList<Genre>? genres = await genresQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);
            // Check if genres are null or empty and return a failure result if so.
            if (genres == null || !genres.Any())
            {
                return Result.Failure<List<GenreDTO>>(new Error("Genres.NotFound", "No genres found", ErrorType.NotFound));
            }

            // Map the genres to their DTO representations.
            var genreDtos = genres.Select(genre => genre.AsGenreDTO()).ToList();

            // Return a success result with the list of genre DTOs.
            return Result.Success(genreDtos);
        }

        private static Expression<Func<Genre, bool>> CreateFilter(SearchParamsDTO searchParams)
        {
            var parameter = Expression.Parameter(typeof(Genre), "genre");
            Expression body = Expression.Equal(
                Expression.Property(parameter, nameof(Genre.IsActive)),
                Expression.Constant(true)
            );

            foreach(var filter in searchParams.SearchParams)
            {
                var propertyInfo = typeof(Genre).GetProperty(filter.PropertyName);
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

            return Expression.Lambda<Func<Genre, bool>>(body,parameter);
        }
    }
}
