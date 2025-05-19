using Application.Abstractions.Messaging;
using Application.Dto;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all tags.
    /// </summary>
    public sealed record GetTagsQuery(SearchParamsDTO SearchParams) : IQuery<List<TagDTO>>;
}
