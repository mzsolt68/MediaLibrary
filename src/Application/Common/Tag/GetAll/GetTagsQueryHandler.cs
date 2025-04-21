using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    public sealed class GetTagsQueryHandler(IUnitOfWork context) : IQueryHandler<GetTagsQuery, List<TagDTO>>
    {
        public async Task<Result<List<TagDTO>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await context.TagRepository.GetAllAsync();

            if (tags == null || !tags.Any())
            {
                return Result.Failure<List<TagDTO>>(new Error("Tags.NotFound", "No tags found", ErrorType.NotFound));
            }

            var tagDtos = tags.Select(tag => tag.AsTagDTO()).ToList();
            return Result.Success(tagDtos);
        }
    }
}
