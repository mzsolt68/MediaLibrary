using Application.Abstractions.Data;
using Application.Books;
using Application.Dto.Books;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetBookByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookRepository> _bookRepository;

        public GetBookByIdQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork.Setup(x => x.BookRepository).Returns(_bookRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBookDTO_WhenBookExists()
        {
            // Arrange
            var book = Book.Create("Title", "2nd", Guid.NewGuid(), "2000", "", Guid.NewGuid()).Value;
            book.AddAuthor(Author.Create("Lastname", "Firstname", "").Value);
            _bookRepository.Setup(x => x.GetByIdAsync(book.Id, CancellationToken.None)).ReturnsAsync(book);

            var query = new GetBookByIdQuery(book.Id);
            var handler = new GetBookByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeOfType<BookDTO>();
            result.Value.Authors.Count.ShouldBe(1);
            result.Value.BookTitle.ShouldBe(book.BookTitle);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _bookRepository.Setup(x => x.GetByIdAsync(bookId, CancellationToken.None)).ReturnsAsync((Book?)null);

            var query = new GetBookByIdQuery(bookId);
            var handler = new GetBookByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Book.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }
    }
}
