using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the creation of a new language.
    /// </summary>
    internal sealed class CreateLanguageCommandHandler(IUnitOfWork context) : ICommandHandler<CreateLanguageCommand, Guid>
    {
        /// <summary>
        /// Handles the command to create a new language.
        /// </summary>
        /// <param name="request">The command containing the details of the language to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// indicating the success or failure of the operation, along with the ID of the created language if successful.
        /// </returns>
        public async Task<Result<Guid>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            // Attempt to create a new language instance.
            var languageResult = Language.Create(request.LanguageName);
            if (languageResult.IsFailure)
            {
                // Return failure result if language creation fails.
                return Result.Failure<Guid>(new Error(languageResult.Error.Code, languageResult.Error.Message, languageResult.Error.Type));
            }

            // Add the created language to the repository.
            context.LanguageRepository.Add(languageResult.Value);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return failure result if saving changes fails.
                return Result.Failure<Guid>(new Error("Language.CreationFailed", "Failed to create language.", ErrorType.Problem));
            }

            // Return success result with the ID of the created language.
            return Result.Success(languageResult.Value.Id);
        }
    }
}
