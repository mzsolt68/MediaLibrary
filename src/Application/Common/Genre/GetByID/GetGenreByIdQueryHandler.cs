using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve a genre by its unique identifier.
    /// </summary>
    public sealed class GetGenreByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetGenreByIdQuery, GenreDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGenreByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The unit of work providing access to repositories.</param>
        public GetGenreByIdQueryHandler(IUnitOfWork context) : base(context) { }

        /// <summary>
        /// Handles the query to retrieve a genre by its unique identifier.
        /// </summary>
        /// <param name="request">The query containing the unique identifier of the genre to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// indicating success or failure, and the retrieved <see cref="GenreDTO"/> if successful.
        /// </returns>
        public async Task<Result<GenreDTO>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the genre by its unique identifier.
            var genre = await context.GenreRepository.GetByIdAsync(request.GenreId);

            // Return a failure result if the genre is not found.
            if (genre == null)
            {
                return Result.Failure<GenreDTO>(new Error("Genre.NotFound", "Genre not found", ErrorType.NotFound));
            }

            // Convert the genre entity to a DTO and return a success result.
            var genreDto = genre.AsGenreDTO();
            return Result.Success(genreDto);
        }
    }
}
