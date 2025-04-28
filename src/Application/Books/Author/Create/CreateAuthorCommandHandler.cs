using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the creation of a new author.
    /// </summary>
    /// <param name="context">The unit of work that provides access to repositories and manages transactions.</param>
    public class CreateAuthorCommandHandler(IUnitOfWork context) : ICommandHandler<CreateAuthorCommand, Guid>
    {
        /// <summary>
        /// Handles the creation of a new author.
        /// </summary>
        /// <param name="request">The command containing the details of the author to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a <see cref="Result{TValue}"/> with the unique identifier of the created author if successful, or an error if the operation fails.
        /// </returns>
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
