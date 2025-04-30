using Application.Abstractions.Messaging;
using Application.Dto.Common;
using System.Linq.Expressions;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all genres.
    /// </summary>
    public sealed record GetGenresQuery<T>(Expression<Func<T, bool>> Predicate) : IQuery<List<GenreDTO>>
    {
    }
}
