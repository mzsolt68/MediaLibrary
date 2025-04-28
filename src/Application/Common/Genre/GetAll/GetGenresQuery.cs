using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all genres.
    /// </summary>
    public sealed class GetGenresQuery : IQuery<List<GenreDTO>>
    {
    }
}
