using Application.Books;
using Application.Abstractions.Data;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class CreateBookFormatCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateBookFormatCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.BookFormatRepository).Returns(new Mock<IBookFormatRepository>().Object);
        }

        [Fact]
        public async Task ProperParametersShouldCreateNewBookFormat()
        {
            // Arrange
            var command = new CreateBookFormatCommand("Hardcover");

            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new CreateBookFormatCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task MissingBookFormatNameShouldReturnFailure()
        {
            // Arrange
            var command = new CreateBookFormatCommand(string.Empty);

            // Act
            var handler = new CreateBookFormatCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("BookFormatName.Required");
        }
    }
}
