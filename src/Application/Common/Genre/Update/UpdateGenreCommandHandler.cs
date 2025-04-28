using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the command to update a genre.
    /// </summary>
    internal sealed class UpdateGenreCommandHandler(IUnitOfWork context) : ICommandHandler<UpdateGenreCommand>
    {
        /// <summary>
        /// Handles the update genre command.
        /// </summary>
        /// <param name="request">The command containing the genre update details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
        public async Task<Result> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the genre by its ID.
            var genre = await context.GenreRepository.GetByIdAsync(request.GenreId);
            if (genre == null)
            {
                // Return failure if the genre is not found.
                return Result.Failure(new Error("Genre.NotFound", $"Genre with ID {request.GenreId} was not found.", ErrorType.NotFound));
            }

            // Attempt to update the genre with the provided details.
            var updateResult = genre.Update(request.GenreName, request.GenreType);
            if (updateResult.IsFailure)
            {
                // Return failure if the update operation fails.
                return Result.Failure(updateResult.Error);
            }

            // Update the genre in the repository.
            await context.GenreRepository.UpdateAsync(genre);

            // Save changes to the database.
            int result = await context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return failure if saving changes fails.
                return Result.Failure(new Error("Genre.UpdateFailed", "Failed to update the genre.", ErrorType.Problem));
            }

            // Return success if the operation completes successfully.
            return Result.Success();
        }
    }
}
