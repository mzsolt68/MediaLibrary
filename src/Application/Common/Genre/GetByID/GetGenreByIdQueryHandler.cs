using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    public sealed class GetGenreByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetGenreByIdQuery, GenreDTO>
    {
        public async Task<Result<GenreDTO>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await context.GenreRepository.GetByIdAsync(request.GenreId);

            if (genre == null)
            {
                return Result.Failure<GenreDTO>(new Error("Genre.NotFound", "Genre not found", ErrorType.NotFound));
            }

            var genreDto = genre.AsGenreDTO();
            return Result.Success(genreDto);
        }
    }
}
