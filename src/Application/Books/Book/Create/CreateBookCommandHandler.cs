using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models.Books;
using SharedKernel;

namespace Application.Books
{
    internal sealed class CreateBookCommandHandler(IUnitOfWork context) : ICommandHandler<CreateBookCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(
                request.BookTitle,
                request.Edition,
                request.PublisherID,
                request.PublishYear,
                request.ISBN,
                request.LanguageID
            );

            if (book.IsFailure)
            {
                return Result.Failure<Guid>(book.Error);
            }

            foreach (var authorID in request.AuthorIDs)
            {
                var authorBook = book.Value.AddAuthor(authorID);
                if (authorBook.IsFailure)
                {
                    return Result.Failure<Guid>(authorBook.Error);
                }
            }

            foreach (var formatID in request.FormatIDs)
            {
                var formatBook = book.Value.AddFormat(formatID);
                if (formatBook.IsFailure)
                {
                    return Result.Failure<Guid>(formatBook.Error);
                }
            }

            foreach (var tagID in request.TagIDs)
            {
                var tagBook = book.Value.AddTag(tagID);
                if (tagBook.IsFailure)
                {
                    return Result.Failure<Guid>(tagBook.Error);
                }
            }

            await context.BookRepository.AddAsync(book.Value);

            int result = await context.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return Result.Failure<Guid>(new Error("Book.CreateFailed", "Failed to create the book.", ErrorType.Failure));
            }

            return Result.Success(book.Value.Id);
        }
    }
}