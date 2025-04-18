using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    public sealed class UpdateAuthorCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateAuthorCommand>
    {
        public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await context.AuthorRepository.GetByIdAsync(request.AuthorId);
            if(author is null)
            {
                return Result.Failure(new Error("Author.NotFound", $"Author with ID {request.AuthorId} not found.", ErrorType.NotFound));
            }

            author.Update(request.FirstName, request.LastName, request.MiddleName);
            var result = await context.SaveChangesAsync(cancellationToken);
            if(result == 0)
            {
                return Result.Failure(new Error("Author.UpdateFailed", "Failed to update author.", ErrorType.Conflict));
            }
            return Result.Success();
        }
    }
}
