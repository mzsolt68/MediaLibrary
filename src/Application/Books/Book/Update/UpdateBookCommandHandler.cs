using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    public class UpdateBookCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateBookCommand>
    {
        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await context.BookRepository.GetByIdAsync(request.BookID);

            if (book == null)
            {
                return Result.Failure(new Error("Book.NotFound", "The book was not found.", ErrorType.NotFound));
            }

            var updateResult = book.Update(
                request.BookTitle,
                request.Edition,
                request.PublisherID,
                request.PublishYear,
                request.ISBN,
                request.LanguageID
            );

            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await context.BookRepository.UpdateAsync(book);

            int result = await context.SaveChangesAsync(cancellationToken);

            if (result <= 0)
            {
                return Result.Failure(new Error("Book.UpdateFailed", "Failed to update the book.", ErrorType.Failure));
            }

            return Result.Success();
        }
    }
}