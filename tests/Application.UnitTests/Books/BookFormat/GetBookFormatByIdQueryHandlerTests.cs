using Application.Abstractions.Data;
using Application.Books;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetBookFormatByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookFormatRepository> _bookFormatRepository;

        public GetBookFormatByIdQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _bookFormatRepository = new Mock<IBookFormatRepository>();
            _unitOfWork.Setup(x => x.BookFormatRepository).Returns(_bookFormatRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBookFormatDTO_WhenBookFormatExists()
        {
            // Arrange
            var bookFormat = BookFormat.Create("Hardcover").Value;
            var query = new GetBookFormatByIdQuery(bookFormat.Id);
            _bookFormatRepository.Setup(x => x.GetByIdAsync(bookFormat.Id, It.IsAny<CancellationToken>())).ReturnsAsync(bookFormat);

            var handler = new GetBookFormatByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.FormatID.ShouldBe(bookFormat.Id);
            result.Value.FormatName.ShouldBe(bookFormat.BookFormatName);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenBookFormatDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new GetBookFormatByIdQuery(id);
            _bookFormatRepository.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Domain.Models.Books.BookFormat?)null);

            var handler = new GetBookFormatByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("BookFormat.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }
    }
}
