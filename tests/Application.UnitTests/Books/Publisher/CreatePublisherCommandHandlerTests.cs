using Application.Abstractions.Data;
using Moq;
using SharedKernel;
using Shouldly;
using Application.Books;

namespace Application.UnitTests.Books
{
    public class CreatePublisherCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreatePublisherCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.PublisherRepository).Returns(new Mock<IPublisherRepository>().Object);
        }

        [Fact]
        public async Task ProperParametersShouldCreateNewPublisher()
        {
            // Arrange
            var command = new CreatePublisherCommand("Sample Publisher");

            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new CreatePublisherCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task MissingPublisherNameShouldReturnFailure()
        {
            // Arrange
            var command = new CreatePublisherCommand(string.Empty); // Invalid

            // Act
            var handler = new CreatePublisherCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PublisherName.Required");
        }
    }
}
