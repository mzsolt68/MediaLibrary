using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the deletion of a book format.
    /// </summary>
    public sealed class DeleteBookFormatCommandHandler : ICommandHandler<DeleteBookFormatCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteBookFormatCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to interact with repositories.</param>
        public DeleteBookFormatCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command to delete a book format.
        /// </summary>
        /// <param name="request">The command containing the ID of the book format to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(DeleteBookFormatCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the book format by its ID.
            var bookFormat = await _unitOfWork.BookFormatRepository.GetByIdAsync(request.BookFormatId);

            // Check if the book format exists.
            if (bookFormat == null)
            {
                return Result.Failure(new Error("BookFormat.NotFound", "The book format was not found.", ErrorType.NotFound));
            }

            // Mark the book format as inactive.
            bookFormat.SetActiveState(false);

            // TODO: Check if the book format is used in any book.

            // Save changes to the database.
            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Check if the save operation was successful.
            if (result <= 0)
            {
                return Result.Failure(new Error("BookFormat.DeleteFailed", "Failed to delete book format.", ErrorType.Conflict));
            }

            return Result.Success();
        }
    }
}
