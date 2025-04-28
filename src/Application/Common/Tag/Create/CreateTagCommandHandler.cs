using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the creation of a new tag.
    /// </summary>
    internal sealed class CreateTagCommandHandler(IUnitOfWork context) : ICommandHandler<CreateTagCommand, Guid>
    {
        /// <summary>
        /// Handles the command to create a new tag.
        /// </summary>
        /// <param name="request">The command containing the tag name to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// with the unique identifier of the created tag if successful, or an error if the operation fails.
        /// </returns>
        public async Task<Result<Guid>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            // Attempt to create a new tag.
            var tagResult = Tag.Create(request.TagName);
            if (tagResult.IsFailure)
            {
                // Return failure result if tag creation fails.
                return Result.Failure<Guid>(new Error(tagResult.Error.Code, tagResult.Error.Message, tagResult.Error.Type));
            }

            // Add the created tag to the repository.
            await context.TagRepository.AddAsync(tagResult.Value);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return failure result if saving changes fails.
                return Result.Failure<Guid>(new Error("Tag.CreateFailed", "Failed to create tag.", ErrorType.Problem));
            }

            // Return success result with the tag's unique identifier.
            return Result.Success(tagResult.Value.Id);
        }
    }
}
