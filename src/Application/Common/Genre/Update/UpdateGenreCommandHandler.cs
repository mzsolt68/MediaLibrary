using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Common;
using SharedKernel;

namespace Application.Common
{
    internal sealed class UpdateGenreCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateGenreCommand>
    {
        public async Task<Result> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await context.Genres.FindAsync(request.GenreId , cancellationToken);
            if (genre == null)
            {
                return Result.Failure(new Error("Genre.NotFound", $"Genre with ID {request.GenreId} was not found.", ErrorType.NotFound));
            }

            var updateResult = genre.Update(request.GenreName, request.GenreType);
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }

            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
