using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the deletion of a genre.
    /// </summary>
    internal sealed class DeleteGenreCommandHandler : ICommandHandler<DeleteGenreCommand>
    {
        private readonly IUnitOfWork _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteGenreCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The unit of work that provides access to repositories and saving changes.</param>
        public DeleteGenreCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the deletion of a genre by its ID.
        /// </summary>
        /// <param name="request">The command containing the ID of the genre to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating the success or failure of the operation.
        /// </returns>
        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the genre by its ID.
            var genre = await _context.GenreRepository.GetByIdAsync(request.GenreId);
            if (genre == null)
            {
                // Return a failure result if the genre is not found.
                return Result.Failure(new Error("Genre.NotFound", $"Genre with {request.GenreId} ID is not found.", ErrorType.NotFound));
            }

            // Mark the genre as inactive.
            genre.SetActiveState(false);

            // Save changes to the database.
            int result = await _context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                // Return a failure result if the deletion operation fails.
                return Result.Failure(new Error("Genre.DeleteFailed", "Failed to delete genre.", ErrorType.Conflict));
            }

            // Return a success result.
            return Result.Success();
        }
    }
}
