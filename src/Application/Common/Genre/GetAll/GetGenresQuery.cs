using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Common;
using System.Linq.Expressions;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all genres.
    /// </summary>
    public sealed record GetGenresQuery<T>(SearchParamsDTO SearchParams) : IQuery<List<GenreDTO>>
    {
    }
}
