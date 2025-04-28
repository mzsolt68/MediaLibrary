using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the deletion of a language by deactivating it and saving the changes.
    /// </summary>
    internal sealed class DeleteLanguageCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteLanguageCommand>
    {
        /// <summary>
        /// Handles the command to delete a language.
        /// </summary>
        /// <param name="request">The command containing the ID of the language to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the language by its ID.
            var language = await context.LanguageRepository.GetByIdAsync(request.LanguageId);
            if (language == null)
            {
                // Return a failure result if the language is not found.
                return Result.Failure(new Error("Language.NotFound", $"Language with {request.LanguageId} ID is not found.", ErrorType.NotFound));
            }

            // Deactivate the language.
            language.SetActiveState(false);

            // Save the changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return a failure result if the changes could not be saved.
                return Result.Failure(new Error("Language.DeletionFailed", "Failed to delete language.", ErrorType.Problem));
            }

            // Return a success result.
            return Result.Success();
        }
    }
}
