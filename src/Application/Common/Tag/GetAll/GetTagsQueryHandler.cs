using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Common;
using Application.Dto.ConvertObjects;
using Application.Extensions;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve all tags.
    /// </summary>
    public sealed class GetTagsQueryHandler(IUnitOfWork context) : IQueryHandler<GetTagsQuery, List<TagDTO>>
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
        public async Task<Result<List<TagDTO>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Tag> tagsQuery;
            int skip = (request.SearchParams.PageNumber - 1) * request.SearchParams.PageSize;

            if (request.SearchParams.SearchParams.Count == 0)
            {
                tagsQuery = context.TagRepository.GetAll();
            }
            else
            {
                tagsQuery = context.TagRepository.GetAll(ExpressionBuilder.CreateFilter<Tag>(request.SearchParams));
            }

            IReadOnlyList<Tag> tags = await tagsQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);

            if (tags == null || !tags.Any())
            {
                return Result.Failure<List<TagDTO>>(new Error("Tags.NotFound", "No tags found", ErrorType.NotFound));
            }

            var tagDtos = tags.Select(tag => tag.AsTagDTO()).ToList();
            return Result.Success(tagDtos);
        }
    }
}
