using Application.Abstractions.Messaging;
using Application.Dto.Common;

namespace Application.Common
{
    public sealed record GetTagByIdQuery(Guid TagId) : IQuery<TagDTO>;
}
