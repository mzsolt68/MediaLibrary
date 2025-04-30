using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;
using Domain.Models.Common;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve all tags.
    /// </summary>
    public sealed class GetTagsQueryHandler(IUnitOfWork context) : IQueryHandler<GetTagsQuery<Tag>, List<TagDTO>>
    {

        /// <summary>
        /// Handles the query to retrieve all tags.
        /// </summary>
        /// <param name="request">The query request to retrieve tags.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{TValue}"/> 
        /// with a list of <see cref="TagDTO"/> if successful, or an error if no tags are found.
        /// </returns>
        public async Task<Result<List<TagDTO>>> Handle(GetTagsQuery<Tag> request, CancellationToken cancellationToken)
        {
            var tags = await context.TagRepository.GetAllAsync(request.Predicate);

            if (tags == null || !tags.Any())
            {
                return Result.Failure<List<TagDTO>>(new Error("Tags.NotFound", "No tags found", ErrorType.NotFound));
            }

            var tagDtos = tags.Select(tag => tag.AsTagDTO()).ToList();
            return Result.Success(tagDtos);
        }
    }
}
