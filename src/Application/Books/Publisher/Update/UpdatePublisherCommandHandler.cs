using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the command to update a publisher.
    /// </summary>
    public sealed class UpdatePublisherCommandHandler : ICommandHandler<UpdatePublisherCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePublisherCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to manage repositories and save changes.</param>
        public UpdatePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the update publisher command.
        /// </summary>
        /// <param name="request">The command containing the publisher ID and updated name.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the publisher by ID.
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(request.PublisherId);

            // Return failure if the publisher is not found.
            if (publisher == null)
            {
                return Result.Failure(new Error("Publisher.NotFound", "The publisher was not found.", ErrorType.NotFound));
            }

            // Attempt to update the publisher's name.
            var updateResult = publisher.Update(request.PublisherName);

            // Return failure if the update operation fails.
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            // Update the publisher in the repository.
            await _unitOfWork.PublisherRepository.UpdateAsync(publisher);

            // Save changes to the database.
            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return failure if no changes were saved.
            if (result <= 0)
            {
                return Result.Failure(new Error("Publisher.UpdateFailed", "Failed to update the publisher.", ErrorType.Failure));
            }

            // Return success if the operation completes successfully.
            return Result.Success();
        }
    }
}
