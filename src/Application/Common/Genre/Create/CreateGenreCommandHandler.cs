using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using Domain.Models.Common;

namespace Application.Common
{
    /// <summary>
    /// Handles the creation of a new genre.
    /// </summary>
    internal sealed class CreateGenreCommandHandler(IUnitOfWork context) : ICommandHandler<CreateGenreCommand, Guid>
    {
        /// <summary>
        /// Handles the creation of a new genre by processing the <see cref="CreateGenreCommand"/>.
        /// </summary>
        /// <param name="request">The command containing the details of the genre to create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// indicating the success or failure of the operation, with the ID of the created genre if successful.
        /// </returns>
        public async Task<Result<Guid>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            // Attempt to create a new genre using the provided details.
            var genreResult = Genre.Create(request.GenreName, request.GenreType);
            if (genreResult.IsFailure)
            {
                // Return a failure result if genre creation fails.
                return Result.Failure<Guid>(new Error(genreResult.Error.Code, genreResult.Error.Message, genreResult.Error.Type));
            }

            // Add the created genre to the repository.
            await context.GenreRepository.AddAsync(genreResult.Value);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return a failure result if saving to the database fails.
                return Result.Failure<Guid>(new Error("Genre.Create", "Saving Genre to database failed", ErrorType.Problem));
            }

            // Return a success result with the ID of the created genre.
            return Result.Success(genreResult.Value.Id);
        }
    }
}
