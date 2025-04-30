using Application.Abstractions.Messaging;
using Application.Dto.Common;
using System.Linq.Expressions;

namespace Application.Common
{
    /// <summary>
    /// Represents a query to retrieve all tags.
    /// </summary>
    public sealed record GetTagsQuery<T>(Expression<Func<T, bool>> Predicate) : IQuery<List<TagDTO>>;
}
