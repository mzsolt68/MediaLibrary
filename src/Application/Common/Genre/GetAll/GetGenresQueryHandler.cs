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
            Expression<Func<Genre, bool>> predicate = genre => genre.IsActive;
            foreach (var filter in searchParams.SearchParams)
            {
                Expression<Func<Genre, bool>> filterExpr = filter.MatchType switch
                {
                    SearchType.Contains => genre =>
                        (genre.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            .Contains(filter.Value),
                    SearchType.Exact => genre =>
                        (genre.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            == filter.Value,
                    SearchType.StartsWith => genre =>
                        (genre.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            .StartsWith(filter.Value),
                    SearchType.EndsWith => genre =>
                        (genre.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            .EndsWith(filter.Value),
                    _ => genre => true
                };
                predicate = predicate.AndAlso(filterExpr);
            }
            return predicate;
        }
    }
}
