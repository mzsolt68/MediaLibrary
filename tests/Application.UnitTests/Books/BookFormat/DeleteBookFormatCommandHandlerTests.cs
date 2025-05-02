using Application.Books;
using Application.Abstractions.Data;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class DeleteBookFormatCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DeleteBookFormatCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.BookFormatRepository).Returns(new Mock<IBookFormatRepository>().Object);
        }

        [Fact]
        public async Task ProperParametersShouldDeleteBookFormat()
        {
            // Arrange
            var bookFormat = BookFormat.Create("Hardcover").Value;
            _unitOfWork.Setup(x => x.BookFormatRepository.GetByIdAsync(bookFormat.Id, CancellationToken.None)).ReturnsAsync(bookFormat);
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new DeleteBookFormatCommand(bookFormat.Id);

            // Act
            var handler = new DeleteBookFormatCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task NonExistentBookFormatShouldReturnNotFound()
        {
            // Arrange
            _unitOfWork.Setup(x => x.BookFormatRepository.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None)).ReturnsAsync(null as BookFormat);

            var command = new DeleteBookFormatCommand(Guid.NewGuid());

            // Act
            var handler = new DeleteBookFormatCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("BookFormat.NotFound");
        }
    }
}
