using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books.Publisher.Delete
{
    /// <summary>
    /// Handles the deletion of a publisher and its associated books.
    /// </summary>
    public sealed class DeletePublisherCommandHandler : ICommandHandler<DeletePublisherCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePublisherCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to manage repositories and save changes.</param>
        public DeletePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to delete a publisher and its associated books.
        /// </summary>
        /// <param name="request">The command containing the publisher ID to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the publisher by ID
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(request.PublisherId);

            // Check if the publisher exists
            if (publisher == null)
            {
                return Result.Failure(new Error("Publisher.NotFound", "The publisher was not found.", ErrorType.NotFound));
            }

            // Mark the publisher as inactive
            publisher.SetActiveState(false);

            // Delete all books associated with the publisher
            await _unitOfWork.PublisherRepository.DeleteBooks(request.PublisherId);

            // Save changes to the database
            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Check if the save operation was successful
            if (result <= 0)
            {
                return Result.Failure(new Error("Publisher.DeleteFailed", "Failed to delete the publisher.", ErrorType.Conflict));
            }

            // Return success result
            return Result.Success();
        }
    }
}
