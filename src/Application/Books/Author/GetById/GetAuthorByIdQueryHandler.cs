using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using SharedKernel;

namespace Application.Books
{
    /// <summary>
    /// Handles queries to retrieve an author by their unique identifier.
    /// </summary>
    /// <remarks>This query handler processes a <see cref="GetAuthorByIdQuery"/> to fetch an author's details
    /// from the underlying data store. If the author is not found, a failure result is returned.</remarks>
    /// <param name="context">The unit of work providing access to the repository layer. This is used to query the author data.</param>
    public sealed class GetAuthorByIdQueryHandler(IUnitOfWork context) : IQueryHandler<GetAuthorByIdQuery, BookAuthorDTO>
    {
        /// <summary>
        /// Handles the query to retrieve an author by their unique identifier.
        /// </summary>
        /// <param name="request">The query containing the unique identifier of the author to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Result{T}"/> containing a <see cref="BookAuthorDTO"/> if the author is found; otherwise, a
        /// failure result with an error indicating that the author was not found.</returns>
        public async Task<Result<BookAuthorDTO>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await context.AuthorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
            if (author == null)
            {
                return Result.Failure<BookAuthorDTO>(new Error("Author.NotFound", "The author was not found in the database.", ErrorType.NotFound));
            }
            return Result.Success(author.AsAuthorDTO());
        }
    }
}
