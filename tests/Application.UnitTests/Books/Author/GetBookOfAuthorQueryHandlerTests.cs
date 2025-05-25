using Application.Abstractions.Data;
using Application.Books;
using Application.Dto.Books;
using Application.Dto.ConvertObjects;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetBookOfAuthorQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IAuthorRepository> _authorRepository;

        public GetBookOfAuthorQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _authorRepository = new Mock<IAuthorRepository>();
            _unitOfWork.Setup(x => x.AuthorRepository).Returns(_authorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfAuthor_DoesntExists()
        {
            var authorID = Guid.NewGuid();
            _authorRepository.Setup(x => x.GetByIdAsync(authorID, CancellationToken.None)).ReturnsAsync((Author?)null);
            var query = new GetBooksOfAuthorQuery(authorID);
            var handler = new GetBooksOfAuthorQueryHandler(_unitOfWork.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            result.IsSuccess.ShouldBe(false);
            result.IsFailure.ShouldBe(true);
            result.Error.Code.ShouldBe("Author.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnBookAuthorDetails_IfAuthor_IsExists()
        {
            var author = Author.Create("Lastname", "Firstname", "").Value;
            var books = new List<Book>
            {
                Book.Create("Title1", "2nd", Guid.NewGuid(), "", "", Guid.NewGuid()).Value,
                Book.Create("Title2", "2nd", Guid.NewGuid(), "", "", Guid.NewGuid()).Value,
                Book.Create("Title3", "2nd", Guid.NewGuid(), "", "", Guid.NewGuid()).Value
            };
            _authorRepository.Setup(x => x.GetByIdAsync(author.Id, CancellationToken.None)).ReturnsAsync(author);
            _authorRepository.Setup(x => x.GetBooksAsync(author.Id, CancellationToken.None)).ReturnsAsync(books);

            var query = new GetBooksOfAuthorQuery(author.Id);
            var handler = new GetBooksOfAuthorQueryHandler(_unitOfWork.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.IsFailure.ShouldBe(false);
            result.IsSuccess.ShouldBe(true);
            result.Value.ShouldBeOfType<BookAuthorDetailsDTO>();
            result.Value.Author.ShouldBeOfType<BookAuthorDTO>();
            result.Value.Author.LastName.ShouldBe(author.AuthorLastName);
            result.Value.Books.Count.ShouldBe(3);
            result.Value.Books.ShouldBeOfType<List<BookDTO>>();
        }
    }
}
