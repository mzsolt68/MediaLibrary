using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handler for retrieving all books published by a specific publisher
    /// </summary>
    public sealed class GetBooksOfPublisherQueryHandler(IUnitOfWork context) : IQueryHandler<GetBooksOfPublisherQuery, BookPublisherDetailsDTO>
    {
        /// <summary>
        /// Handles the query to retrieve all books associated with a specific publisher
        /// </summary>
        /// <param name="request">The query containing the publisher ID</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests</param>
        /// <returns>A result containing the publisher details and their associated books</returns>
        public async Task<Result<BookPublisherDetailsDTO>> Handle(GetBooksOfPublisherQuery request, CancellationToken cancellationToken)
        {
            var publisher = await context.PublisherRepository.GetByIdAsync(request.PublisherId, cancellationToken);

            if (publisher == null)
            {
                return Result.Failure<BookPublisherDetailsDTO>(new Error(
                    "Publisher.NotFound",
                    "The publisher was not found in the database.",
                    ErrorType.NotFound));
            }

            var publisherBooks = await context.PublisherRepository.GetPublishersBooksAsync(request.PublisherId, cancellationToken);
            BookPublisherDetailsDTO result = new BookPublisherDetailsDTO
            {
                Publisher = publisher.AsPublisherDTO(),
                Books = publisherBooks.Select(b => b.AsBookDTO()).ToList()
            };

            return Result.Success(result);
        }
    }
}