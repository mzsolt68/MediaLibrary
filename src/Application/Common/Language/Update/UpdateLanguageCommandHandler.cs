using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the command to update a language.
    /// </summary>
    internal sealed class UpdateLanguageCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateLanguageCommand>
    {
        /// <summary>
        /// Handles the update language command.
        /// </summary>
        /// <param name="request">The command containing the language update details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the language by its ID.
            var language = await context.LanguageRepository.GetByIdAsync(request.LanguageId);
            if (language == null)
            {
                // Return failure if the language is not found.
                return Result.Failure(new Error("Language.NotFound", $"Language with ID {request.LanguageId} was not found.", ErrorType.NotFound));
            }

            // Attempt to update the language.
            var updateResult = language.Update(request.LanguageName);
            if (updateResult.IsFailure)
            {
                // Return failure if the update operation fails.
                return Result.Failure(updateResult.Error);
            }

            // Update the language in the repository.
            context.LanguageRepository.Update(language);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return failure if no changes were saved.
                return Result.Failure(new Error("Language.UpdateFailed", "Failed to update language.", ErrorType.Problem));
            }

            // Return success if the operation completes successfully.
            return Result.Success();
        }
    }
}
