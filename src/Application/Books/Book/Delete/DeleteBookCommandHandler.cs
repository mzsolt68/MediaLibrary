using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    public sealed class DeleteBookCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteBookCommand>
    {
        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await context.BookRepository.GetByIdAsync(request.BookId);

            if (book is null)
            {
                return Result.Failure(new Error("Book.NotFound", $"Book with {request.BookId} ID is not found.", ErrorType.NotFound));
            }

            await context.BookRepository.DeleteBookAuthorsAsync(book.Id);
            await context.BookRepository.DeleteBookFormatsAsync(book.Id);
            await context.BookRepository.DeleteBookTagsAsync(book.Id);

            book.SetActiveState(false);
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return Result.Failure(new Error("Book.DeleteFailed", "Failed to delete book.", ErrorType.Conflict));
            }

            return Result.Success();
        }
    }
}
