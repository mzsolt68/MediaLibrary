
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using Domain.Models.Common;

namespace Application.Common.Genre.Create
{
    internal sealed class CreateGenreCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateGenreCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genreResult = Domain.Models.Common.Genre.Create(request.GenreName, request.GenreType);
            if(genreResult.IsFailure)
            {
                return Result.Failure<Guid>(new Error(genreResult.Error.Code, genreResult.Error.Message, genreResult.Error.Type));
            }
            context.Genres.Add(genreResult.Value);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success(genreResult.Value.Id);
        }
    }
}
