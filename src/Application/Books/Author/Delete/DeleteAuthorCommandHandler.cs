using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    public sealed class DeleteAuthorCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteAuthorCommand>
    {
        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await context.AuthorRepository.GetByIdAsync(request.bookId);
            if(author is null)
            {
                return Result.Failure(new Error("Author.NotFound", $"Author with {request.bookId} ID was not found", ErrorType.NotFound));
            }

            await context.AuthorRepository.DeleteBooks(author.Id);

            author.SetActiveState(false);
            int result = await context.SaveChangesAsync(cancellationToken);
            if(result == 0)
            {
                return Result.Failure(new Error("Author.DeleteFailed", "Author delete failed", ErrorType.Problem));
            }
            return Result.Success();
        }
    }
}
