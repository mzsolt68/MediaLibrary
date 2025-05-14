using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Common;
using Application.Dto.ConvertObjects;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Extensions;
using System.Linq.Expressions;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve all languages.
    /// </summary>
    /// <param name="context">The unit of work providing access to repositories.</param>
    public sealed class GetLanguagesQueryHandler(IUnitOfWork context) : IQueryHandler<GetLanguagesQuery<Language>, List<LanguageDTO>>
    {
        /// <summary>
        /// Handles the query to retrieve all languages.
        /// </summary>
        /// <param name="request">The query request containing any necessary parameters.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// with a list of <see cref="LanguageDTO"/> if successful, or an error if no languages are found.
        /// </returns>
        public async Task<Result<List<LanguageDTO>>> Handle(GetLanguagesQuery<Language> request, CancellationToken cancellationToken)
        {
            IQueryable<Language> languagesQuery;
            int skip = (request.SearchParams.PageNumber - 1) * request.SearchParams.PageNumber;

            if(request.SearchParams.SearchParams.Count == 0)
            {
                languagesQuery = context.LanguageRepository.GetAll();
            }
            else
            {
                languagesQuery = context.LanguageRepository.GetAll(CreateFilter(request.SearchParams));
            }
            // Retrieve all languages from the repository.
            IReadOnlyList<Language> languages = await languagesQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);

            // Check if no languages were found.
            if (languages == null || !languages.Any())
            {
                return Result.Failure<List<LanguageDTO>>(new Error("Languages.NotFound", "No languages found", ErrorType.NotFound));
            }

            // Convert the languages to DTOs.
            var languageDtos = languages.Select(language => language.AsLanguageDTO()).ToList();

            // Return the successful result with the list of language DTOs.
            return Result.Success(languageDtos);
        }

        private static Expression<Func<Language, bool>> CreateFilter(SearchParamsDTO searchParams)
        {
            Expression<Func<Language, bool>> predicate = genre => genre.IsActive;
            foreach (var filter in searchParams.SearchParams)
            {
                Expression<Func<Language, bool>> filterExpr = filter.MatchType switch
                {
                    SearchType.Contains => language =>
                        (language.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            .Contains(filter.Value),
                    SearchType.Exact => language =>
                        (language.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            == filter.Value,
                    SearchType.StartsWith => language =>
                        (language.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            .StartsWith(filter.Value),
                    SearchType.EndsWith => language =>
                        (language.GetPropertyValue(filter.PropertyName)!.ToString() ?? string.Empty)
                            .EndsWith(filter.Value),
                    _ => language => true
                };
                predicate = predicate.AndAlso(filterExpr);
            }
            return predicate;
        }
    }
}
