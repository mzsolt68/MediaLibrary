using Application.Books;
using Application.Abstractions.Data;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class UpdatePublisherCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UpdatePublisherCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.PublisherRepository).Returns(new Mock<IPublisherRepository>().Object);
        }

        [Fact]
        public async Task ProperParametersShouldUpdatePublisher()
        {
            // Arrange
            var publisher = Publisher.Create("Original Publisher").Value;
            _unitOfWork.Setup(x => x.PublisherRepository.GetByIdAsync(publisher.Id)).ReturnsAsync(publisher);
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new UpdatePublisherCommand(publisher.Id, "Updated Publisher");

            // Act
            var handler = new UpdatePublisherCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task NonExistentPublisherShouldReturnNotFound()
        {
            // Arrange
            _unitOfWork.Setup(x => x.PublisherRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Publisher);

            var command = new UpdatePublisherCommand(Guid.NewGuid(), "Updated Publisher");

            // Act
            var handler = new UpdatePublisherCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.NotFound);
            result.Error.Code.ShouldBe("Publisher.NotFound");
        }

        [Fact]
        public async Task SaveChangeFailureShouldReturnConflict()
        {
            // Arrange
            var publisher = Publisher.Create("Original Publisher").Value;
            _unitOfWork.Setup(x => x.PublisherRepository.GetByIdAsync(publisher.Id)).ReturnsAsync(publisher);
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var command = new UpdatePublisherCommand(publisher.Id, "Updated Publisher");

            // Act
            var handler = new UpdatePublisherCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
        }
    }
}
