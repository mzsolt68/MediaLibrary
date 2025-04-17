using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Common
{
    internal sealed class DeleteGenreCommandHandler(IUnitOfWork context) : ICommandHandler<DeleteGenreCommand>
    {
        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await context.GenreRepository.GetByIdAsync(request.GenreId);
            if (genre == null)
            {
                return Result.Failure(new Error("Genre.NotFound", $"Genre with {request.GenreId} ID is not found.", ErrorType.NotFound));
            }
            genre.SetActiveState(false);
            int result = await context.SaveChangesAsync(cancellationToken);
            if(result == 0)
            {
                return Result.Failure(new Error("Genre.DeleteFailed", "Failed to delete genre.", ErrorType.Conflict));
            }
            return Result.Success();
        }
    }
}
