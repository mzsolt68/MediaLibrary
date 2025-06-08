using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles queries to retrieve a publisher by its unique identifier.
    /// </summary>
    /// <remarks>This query handler fetches a publisher from the database using the provided publisher ID. If
    /// the publisher is not found, a failure result is returned with an appropriate error message.</remarks>
    /// <param name="context"></param>
    public sealed class GetPublisherByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetPublisherByIdQuery, BookPublisherDTO>
    {
        /// <summary>
        /// Handles the query to retrieve a publisher by its unique identifier.
        /// </summary>
        /// <remarks>This method queries the database for a publisher with the specified identifier. If
        /// the publisher does not exist, the result will indicate failure with an appropriate error message.</remarks>
        /// <param name="request">The query containing the identifier of the publisher to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing the publisher's data as a <see cref="BookPublisherDTO"/> if found;
        /// otherwise, a failure result with an error indicating that the publisher was not found.</returns>
        public async Task<Result<BookPublisherDTO>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            var publisher = await context.PublisherRepository.GetByIdAsync(request.PublisherId, cancellationToken);
            if (publisher == null)
            {
                return Result.Failure<BookPublisherDTO>(new Error("Publisher.NotFound", "The publisher was not found in the database.", ErrorType.NotFound));
            }
            return Result.Success(publisher.AsPublisherDTO());
        }
    }
}
