using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    public sealed class GetTagByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetTagByIdQuery, TagDTO>
    {
        public async Task<Result<TagDTO>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await context.TagRepository.GetByIdAsync(request.TagId);

            if (tag == null)
            {
                return Result.Failure<TagDTO>(new Error("Tag.NotFound", "Tag not found", ErrorType.NotFound));
            }

            return Result.Success(tag.AsTagDTO());
        }
    }
}
