using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the command to update a book's details.
    /// </summary>
    /// <param name="context">The unit of work providing access to repositories and data operations.</param>
    public class UpdateBookCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateBookCommand>
    {
        /// <summary>
        /// Handles the update book command.
        /// </summary>
        /// <param name="request">The command containing the details of the book to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the book by its ID.
            var book = await context.BookRepository.GetByIdAsync(request.BookDTO.BookID);

            // Return failure if the book is not found.
            if (book == null)
            {
                return Result.Failure(new Error("Book.NotFound", "The book was not found.", ErrorType.NotFound));
            }

            // Attempt to update the book's details.
            var updateResult = book.Update(
                request.BookDTO.BookTitle,
                request.BookDTO.Edition,
                request.BookDTO.PublisherID,
                request.BookDTO.PublishYear,
                request.BookDTO.ISBN,
                request.BookDTO.LanguageID
            );

            // Return failure if the update operation fails.
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            // Update the book in the repository.
            context.BookRepository.Update(book);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);

            // Return failure if saving changes fails.
            if (result <= 0)
            {
                return Result.Failure(new Error("Book.UpdateFailed", "Failed to update the book.", ErrorType.Failure));
            }

            // Return success if the operation completes successfully.
            return Result.Success();
        }
    }
}