using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve a tag by its unique identifier.
    /// </summary>
    /// <param name="TagId">The unique identifier of the tag to retrieve.</param>
    /// <returns>A <see cref="TagDTO"/> containing the details of the tag.</returns>
    public sealed record GetTagByIdQuery(Guid TagId) : IQuery<TagDTO>;
}
