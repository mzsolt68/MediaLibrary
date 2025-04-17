
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;
using Domain.Models.Common;

namespace Application.Common
{
    internal sealed class CreateGenreCommandHandler(IUnitOfWork context) : ICommandHandler<CreateGenreCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genreResult = Genre.Create(request.GenreName, request.GenreType);
            if(genreResult.IsFailure)
            {
                return Result.Failure<Guid>(new Error(genreResult.Error.Code, genreResult.Error.Message, genreResult.Error.Type));
            }
            await context.GenreRepository.AddAsync(genreResult.Value);
            int result = await context.SaveChangesAsync(cancellationToken);
            if(result == 0)
            {
                return Result.Failure<Guid>(new Error("Genre.Create", "Saving Genre to database failed", ErrorType.Problem));
            }
            return Result.Success(genreResult.Value.Id);
        }
    }
}
