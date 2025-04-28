using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve a tag by its ID.
    /// </summary>
    /// <param name="context">The unit of work providing access to repositories.</param>
    public sealed class GetTagByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetTagByIdQuery, TagDTO>
    {
        /// <summary>
        /// Handles the query to retrieve a tag by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the tag to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the <see cref="TagDTO"/> if found, 
        /// or an error if the tag is not found.
        /// </returns>
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
