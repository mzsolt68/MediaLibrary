using Application.Abstractions.Data;
using Domain.Models.Books;
using Moq;
using Shouldly;
using Application.Books;
using System.Linq.Expressions;

namespace Application.UnitTests.Books
{
    public class GetBooksOfFormatQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookFormatRepository> _bookFormatRepository;

        public GetBooksOfFormatQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _bookFormatRepository = new Mock<IBookFormatRepository>();
            _unitOfWork.Setup(x => x.BookFormatRepository).Returns(_bookFormatRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfBookDTOs_WhenBooksExistForFormat()
        {
            // Arrange
            var formatId = Guid.NewGuid();
            var books = new List<Book>
            {
                Book.Create("Title1", "1st", Guid.NewGuid(), "2020", "", Guid.NewGuid()).Value,
                Book.Create("Title2", "2nd", Guid.NewGuid(), "2021", "", Guid.NewGuid()).Value
            };
            _bookFormatRepository.Setup(x => x.Exists(It.IsAny<Expression<Func<BookFormat, bool>>>())).ReturnsAsync(true);

            _bookFormatRepository.Setup(x => x.GetBooksOfFormat(formatId, It.IsAny<CancellationToken>())).ReturnsAsync(books);

            var query = new GetBooksOfFormatQuery(formatId);
            var handler = new GetBooksOfFormatQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value[0].BookTitle.ShouldBe("Title1");
            result.Value[1].BookTitle.ShouldBe("Title2");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenFormatDoesNotExist()
        {
            // Arrange
            var formatId = Guid.NewGuid();
            _bookFormatRepository.Setup(x => x.Exists(It.IsAny<Expression<Func<BookFormat, bool>>>())).ReturnsAsync(false);

            var query = new GetBooksOfFormatQuery(formatId);
            var handler = new GetBooksOfFormatQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Format.NotFound");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoBooksExistForFormat()
        {
            // Arrange
            var formatId = Guid.NewGuid();
            _bookFormatRepository.Setup(x => x.Exists(It.IsAny<Expression<Func<BookFormat, bool>>>())).ReturnsAsync(true);
            _bookFormatRepository.Setup(x => x.GetBooksOfFormat(formatId, It.IsAny<CancellationToken>())).ReturnsAsync(new List<Book>());

            var query = new GetBooksOfFormatQuery(formatId);
            var handler = new GetBooksOfFormatQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Book.NotFound");
        }
    }
}
