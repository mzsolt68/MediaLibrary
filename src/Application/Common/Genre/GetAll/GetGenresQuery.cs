using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Common;
using System.Linq.Expressions;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve a list of genres based on the specified search parameters.
    /// </summary>
    /// <param name="SearchParams">The search parameters used to filter the genres. This parameter cannot be null.</param>
    public sealed record GetGenresQuery(SearchParamsDTO SearchParams) : IQuery<List<GenreDTO>>;
}
