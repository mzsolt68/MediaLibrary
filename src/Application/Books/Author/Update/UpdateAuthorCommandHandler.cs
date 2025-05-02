using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles the command to update an author's details.
    /// </summary>
    public sealed class UpdateAuthorCommandHandler : ICommandHandler<UpdateAuthorCommand>
    {
        private readonly IUnitOfWork _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAuthorCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The unit of work to interact with the data layer.</param>
        public UpdateAuthorCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the update author command.
        /// </summary>
        /// <param name="request">The command containing the author update details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the author by ID
            var author = await _context.AuthorRepository.GetByIdAsync(request.AuthorId);
            if (author is null)
            {
                // Return failure if the author is not found
                return Result.Failure(new Error("Author.NotFound", $"Author with ID {request.AuthorId} not found.", ErrorType.NotFound));
            }

            // Update the author's details
            var updateResult = author.Update(request.FirstName, request.LastName, request.MiddleName);
            if(updateResult.IsFailure)
            {
                // Return failure if the update operation fails
                return Result.Failure(updateResult.Error);
            }
            // Update the author in the repository
            _context.AuthorRepository.Update(author);

            // Save changes to the database
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return failure if the update operation fails
                return Result.Failure(new Error("Author.UpdateFailed", "Failed to update author.", ErrorType.Conflict));
            }

            // Return success if the operation completes successfully
            return Result.Success();
        }
    }
}
