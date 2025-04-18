using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    public class CreateAuthorCommandHandler(IUnitOfWork context) : ICommandHandler<CreateAuthorCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            // Create the Author entity
            var authorResult = Author.Create(request.LastName, request.FirstName, request.MiddleName);

            if (authorResult.IsFailure)
            {
                return Result.Failure<Guid>(authorResult.Error);
            }

            // Add the Author to the repository
            await context.AuthorRepository.AddAsync(authorResult.Value);

            // Save changes
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                return Result.Failure<Guid>(new Error("Author.CreateFailed", "Failed to create author.", ErrorType.Failure));
            }

            return Result.Success(authorResult.Value.Id);
        }
    }
}
