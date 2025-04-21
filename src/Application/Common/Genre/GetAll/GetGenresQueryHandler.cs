using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    public sealed class GetGenresQueryHandler(IUnitOfWork context) : IQueryHandler<GetGenresQuery, List<GenreDTO>>
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public GetGenresQueryHandler(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public async Task<Result<List<GenreDTO>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await context.GenreRepository.GetAllAsync();

            if (genres == null || !genres.Any())
            {
                return Result.Failure<List<GenreDTO>>(new Error("Genres.NotFound", "No genres found", ErrorType.NotFound));
            }

            var genreDtos = genres.Select(genre => genre.AsGenreDTO()).ToList();
            return Result.Success(genreDtos);
        }
    }
}
