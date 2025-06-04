using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve a genre by its unique identifier.
    /// </summary>
    /// <param name="GenreId">The unique identifier of the genre to retrieve. Must not be an empty <see cref="Guid"/>.</param>
    public sealed record GetGenreByIdQuery(Guid GenreId) : IQuery<GenreDTO>;
}
