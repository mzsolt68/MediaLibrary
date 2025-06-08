using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using Application.Extensions;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles queries to retrieve a paginated list of publishers based on specified search parameters.
    /// </summary>
    /// <remarks>This query handler processes the <see cref="GetPublishersQuery"/> to fetch publishers from
    /// the database. It supports pagination and filtering based on the provided search parameters. If no publishers
    /// match the criteria, a failure result is returned.</remarks>
    /// <param name="context">The unit of work used to access the repository for publishers.</param>
    public sealed class GetPublishersQueryHandler(IUnitOfWork context) : IQueryHandler<GetPublishersQuery, List<BookPublisherDTO>>
    {
        /// <summary>
        /// Handles the retrieval of publishers based on the specified query parameters.
        /// </summary>
        /// <remarks>This method supports pagination and filtering based on the provided search
        /// parameters. If no filters are specified, all publishers are retrieved.</remarks>
        /// <param name="request">The query containing search parameters for filtering and pagination.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="BookPublisherDTO"/> objects representing the
        /// publishers. Returns a failure result if no publishers are found.</returns>
        public async Task<Result<List<BookPublisherDTO>>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Publisher> publishersQuery;
            int skip = (request.SearchParams.PageNumber - 1) * request.SearchParams.PageSize;

            if (request.SearchParams.SearchParams.Count == 0)
            {
                publishersQuery = context.PublisherRepository.GetAll();
            }
            else
            {
                publishersQuery = context.PublisherRepository.GetAll(ExpressionBuilder.CreateFilter<Publisher>(request.SearchParams));
            }

            IReadOnlyList<Publisher>? publishers = await publishersQuery.Skip(skip).Take(request.SearchParams.PageSize).ToListAsync(cancellationToken);

            if (publishers == null || !publishers.Any())
            {
                return Result.Failure<List<BookPublisherDTO>>(new Error("Publishers.NotFound", "No publishers found in the database.", ErrorType.NotFound));
            }

            var publisherDtos = publishers.Select(publisher => publisher.AsPublisherDTO()).ToList();

            return Result.Success(publisherDtos);
        }
    }
}
