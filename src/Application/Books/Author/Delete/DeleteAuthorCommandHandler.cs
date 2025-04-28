using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the deletion of an author.
    /// </summary>
    /// <param name="context">The unit of work that provides access to repositories and manages transactions.</param>
    public sealed class DeleteAuthorCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteAuthorCommand>
    {
        /// <summary>
        /// Handles the command to delete an author.
        /// </summary>
        /// <param name="request">The command containing the ID of the author to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the author by ID.
            var author = await context.AuthorRepository.GetByIdAsync(request.bookId);
            if (author is null)
            {
                // Return failure if the author is not found.
                return Result.Failure(new Error("Author.NotFound", $"Author with {request.bookId} ID was not found", ErrorType.NotFound));
            }

            // Delete all books associated with the author.
            await context.AuthorRepository.DeleteBooks(author.Id);

            // Mark the author as inactive.
            author.SetActiveState(false);

            // TODO: Check if the author is used in any book.

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return failure if the delete operation fails.
                return Result.Failure(new Error("Author.DeleteFailed", "Author delete failed", ErrorType.Problem));
            }

            // Return success if the operation completes successfully.
            return Result.Success();
        }
    }
}
