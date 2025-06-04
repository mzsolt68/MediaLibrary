using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the command to update a book format.
    /// </summary>
    public sealed class UpdateBookFormatCommandHandler : ICommandHandler<UpdateBookFormatCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBookFormatCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to manage repositories and save changes.</param>
        public UpdateBookFormatCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the update book format command.
        /// </summary>
        /// <param name="request">The command containing the book format update details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a <see cref="Result"/> indicating success or failure.
        /// </returns>
        public async Task<Result> Handle(UpdateBookFormatCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the book format by its ID.
            var bookFormat = await _unitOfWork.BookFormatRepository.GetByIdAsync(request.BookFormatId, cancellationToken);

            // Return failure if the book format is not found.
            if (bookFormat == null)
            {
                return Result.Failure(new Error("BookFormat.NotFound", "The book format was not found.", ErrorType.NotFound));
            }

            // Attempt to update the book format.
            var updateResult = bookFormat.Update(request.BookFormatName);

            // Return failure if the update operation fails.
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            // Update the book format in the repository.
            _unitOfWork.BookFormatRepository.Update(bookFormat);

            // Save changes to the database.
            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return failure if saving changes fails.
            if (result <= 0)
            {
                return Result.Failure(new Error("BookFormat.UpdateFailed", "Failed to update book format.", ErrorType.Failure));
            }

            // Return success if the operation completes successfully.
            return Result.Success();
        }
    }
}
