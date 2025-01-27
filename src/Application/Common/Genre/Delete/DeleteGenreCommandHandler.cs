using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Common
{
    internal sealed class DeleteGenreCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteGenreCommand>
    {
        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await context.Genres.FindAsync(request.GenreId, cancellationToken);
            if (genre == null)
            {
                return Result.Failure(new Error("Genre.NotFound", $"Genre with {request.GenreId} ID is not found.", ErrorType.NotFound));
            }
            genre.Inactivate();
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
