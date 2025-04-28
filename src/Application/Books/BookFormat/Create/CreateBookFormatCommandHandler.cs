using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the creation of a new book format.
    /// </summary>
    public sealed class CreateBookFormatCommandHandler : ICommandHandler<CreateBookFormatCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBookFormatCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to manage repositories and save changes.</param>
        public CreateBookFormatCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the creation of a new book format.
        /// </summary>
        /// <param name="request">The command containing the book format name.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// with the ID of the created book format if successful, or an error if the operation fails.
        /// </returns>
        public async Task<Result<Guid>> Handle(CreateBookFormatCommand request, CancellationToken cancellationToken)
        {
            // Attempt to create a new book format.
            var bookFormatResult = BookFormat.Create(request.BookFormatName);

            // Return failure if the book format creation failed.
            if (bookFormatResult.IsFailure)
            {
                return Result.Failure<Guid>(bookFormatResult.Error);
            }

            // Add the new book format to the repository.
            await _unitOfWork.BookFormatRepository.AddAsync(bookFormatResult.Value);

            // Save changes to the database.
            int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return failure if saving changes failed.
            if (result <= 0)
            {
                return Result.Failure<Guid>(new Error("BookFormat.CreateFailed", "Failed to create book format.", ErrorType.Failure));
            }

            // Return success with the ID of the created book format.
            return Result.Success(bookFormatResult.Value.Id);
        }
    }
}
