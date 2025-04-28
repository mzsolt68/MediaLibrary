using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the command to delete a tag by setting its active state to false.
    /// </summary>
    internal sealed class DeleteTagCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteTagCommand>
    {
        /// <summary>
        /// Handles the deletion of a tag.
        /// </summary>
        /// <param name="request">The command containing the ID of the tag to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the tag by its ID.
            var tag = await context.TagRepository.GetByIdAsync(request.TagId);
            if (tag == null)
            {
                // Return a failure result if the tag is not found.
                return Result.Failure(new Error("Tag.NotFound", $"Tag with {request.TagId} ID is not found.", ErrorType.NotFound));
            }

            // Set the tag's active state to false.
            tag.SetActiveState(false);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return a failure result if the save operation fails.
                return Result.Failure(new Error("Tag.DeleteFailed", "Failed to delete tag.", ErrorType.Problem));
            }

            // Return a success result.
            return Result.Success();
        }
    }
}
