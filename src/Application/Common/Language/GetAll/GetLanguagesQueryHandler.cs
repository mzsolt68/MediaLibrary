using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve all languages.
    /// </summary>
    /// <param name="context">The unit of work providing access to repositories.</param>
    public sealed class GetLanguagesQueryHandler(IUnitOfWork context) : IQueryHandler<GetLanguagesQuery, List<LanguageDTO>>
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
        public async Task<Result<List<LanguageDTO>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all languages from the repository.
            var languages = await context.LanguageRepository.GetAllAsync();

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
    }
}
