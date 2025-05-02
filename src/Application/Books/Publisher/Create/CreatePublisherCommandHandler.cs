using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the creation of a publisher.
    /// </summary>
    public sealed class CreatePublisherCommandHandler : ICommandHandler<CreatePublisherCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePublisherCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to manage repositories and save changes.</param>
        public CreatePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the creation of a publisher.
        /// </summary>
        /// <param name="request">The command containing the publisher name.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// with the unique identifier of the created publisher if successful, or an error if the operation fails.
        /// </returns>
        public async Task<Result<Guid>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            // Create the Publisher entity
            var publisherResult = Domain.Models.Books.Publisher.Create(request.PublisherName);

            if (publisherResult.IsFailure)
            {
                return Result.Failure<Guid>(publisherResult.Error);
            }

            // Add the Publisher to the repository
            _unitOfWork.PublisherRepository.Add(publisherResult.Value);

            // Save changes
            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                return Result.Failure<Guid>(new Error("Publisher.CreateFailed", "Failed to create publisher.", ErrorType.Failure));
            }

            return Result.Success(publisherResult.Value.Id);
        }
    }
}
