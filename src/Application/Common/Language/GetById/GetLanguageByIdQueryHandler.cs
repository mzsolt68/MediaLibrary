using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve a language by its ID.
    /// </summary>
    /// <param name="context">The unit of work providing access to repositories.</param>
    public sealed class GetLanguageByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetLanguageByIdQuery, LanguageDTO>
    {
        /// <summary>
        /// Handles the query to retrieve a language by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the language to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{LanguageDTO}"/> 
        /// indicating success or failure, along with the retrieved language data if successful.
        /// </returns>
        public async Task<Result<LanguageDTO>> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
        {
            var language = await context.LanguageRepository.GetByIdAsync(request.LanguageId);

            if (language == null)
            {
                return Result.Failure<LanguageDTO>(new Error("Language.NotFound", "Language not found", ErrorType.NotFound));
            }

            return Result.Success(language.AsLanguageDTO());
        }
    }
}
