using Application.Abstractions.Messaging;
using Application.Dto.Common;
using SharedKernel;
using Application.Abstractions.Data;
using Application.Dto.ConvertObjects;

namespace Application.Common
{
    /// <summary>
    /// Handles the query to retrieve all tags.
    /// </summary>
    public sealed class GetTagsQueryHandler(IUnitOfWork context) : IQueryHandler<GetTagsQuery, List<TagDTO>>
    {
        private readonly IUnitOfWork _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagsQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The unit of work to access repositories.</param>
        public GetTagsQueryHandler(IUnitOfWork context)
        {
            _context = context;
        }

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
            var tags = await _context.TagRepository.GetAllAsync();

            if (tags == null || !tags.Any())
            {
                return Result.Failure<List<TagDTO>>(new Error("Tags.NotFound", "No tags found", ErrorType.NotFound));
            }

            var tagDtos = tags.Select(tag => tag.AsTagDTO()).ToList();
            return Result.Success(tagDtos);
        }
    }
}
