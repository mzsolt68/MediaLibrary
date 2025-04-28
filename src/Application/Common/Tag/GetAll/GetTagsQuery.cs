using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all tags.
    /// </summary>
    public sealed record GetTagsQuery : IQuery<List<TagDTO>>;
}
