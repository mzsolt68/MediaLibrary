using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the deletion of a book by processing the <see cref="DeleteBookCommand"/>.
    /// </summary>
    public sealed class DeleteBookCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteBookCommand>
    {
        /// <summary>
        /// Handles the deletion of a book and its associated data.
        /// </summary>
        /// <param name="request">The command containing the ID of the book to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the book by its ID.
            var book = await context.BookRepository.GetByIdAsync(request.BookId);

            // Return a failure result if the book is not found.
            if (book is null)
            {
                return Result.Failure(new Error("Book.NotFound", $"Book with {request.BookId} ID is not found.", ErrorType.NotFound));
            }

            // Delete associated authors, formats, and tags of the book.
            context.BookRepository.DeleteBookAuthors(book.Id);
            context.BookRepository.DeleteBookFormats(book.Id);
            context.BookRepository.DeleteBookTags(book.Id);

            // Mark the book as inactive.
            book.SetActiveState(false);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);

            // Return a failure result if the save operation fails.
            if (result == 0)
            {
                return Result.Failure(new Error("Book.DeleteFailed", "Failed to delete book.", ErrorType.Conflict));
            }

            // Return a success result.
            return Result.Success();
        }
    }
}
