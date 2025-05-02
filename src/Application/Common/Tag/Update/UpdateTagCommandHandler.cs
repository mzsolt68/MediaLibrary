using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the command to update a tag.
    /// </summary>
    internal sealed class UpdateTagCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateTagCommand>
    {
        /// <summary>
        /// Handles the update tag command.
        /// </summary>
        /// <param name="request">The command containing the tag ID and new tag name.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the tag by its ID.
            var tag = await context.TagRepository.GetByIdAsync(request.TagId);
            if (tag == null)
            {
                // Return failure if the tag is not found.
                return Result.Failure(new Error("Tag.NotFound", $"Tag with ID {request.TagId} was not found.", ErrorType.NotFound));
            }

            // Attempt to update the tag.
            var updateResult = tag.Update(request.TagName);
            if (updateResult.IsFailure)
            {
                // Return failure if the update operation fails.
                return Result.Failure(updateResult.Error);
            }
            // Mark the tag as modified in the context.
            context.TagRepository.Update(tag);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return failure if no changes were saved.
                return Result.Failure(new Error("Tag.UpdateFailed", "Failed to update tag.", ErrorType.Problem));
            }

            // Return success if the operation completes successfully.
            return Result.Success();
        }
    }
}
