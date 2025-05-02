using Application.Books.Publisher.Delete;
using Application.Abstractions.Data;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;
using System.Linq.Expressions;

namespace Application.UnitTests.Books
{
    public class DeletePublisherCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DeletePublisherCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.PublisherRepository).Returns(new Mock<IPublisherRepository>().Object);
        }

        [Fact]
        public async Task ProperParametersShouldDeletePublisher()
        {
            // Arrange
            var publisher = Publisher.Create("Sample Publisher").Value;
            _unitOfWork.Setup(x => x.PublisherRepository.GetByIdAsync(publisher.Id)).ReturnsAsync(publisher);
            _unitOfWork.Setup(x => x.BookRepository.GetAllAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(new List<Book>());
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new DeletePublisherCommand(publisher.Id);

            // Act
            var handler = new DeletePublisherCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            publisher.IsActive.ShouldBeFalse();
        }

        [Fact]
        public async Task NonExistentPublisherShouldReturnNotFound()
        {
            // Arrange
            _unitOfWork.Setup(x => x.PublisherRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Publisher);

            var command = new DeletePublisherCommand(Guid.NewGuid());

            // Act
            var handler = new DeletePublisherCommandHandler(_unitOfWork.Object);
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
            var publisher = Publisher.Create("Sample Publisher").Value;
            _unitOfWork.Setup(x => x.PublisherRepository.GetByIdAsync(publisher.Id)).ReturnsAsync(publisher);
            _unitOfWork.Setup(x => x.BookRepository.GetAllAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(new List<Book>());
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var command = new DeletePublisherCommand(publisher.Id);

            // Act
            var handler = new DeletePublisherCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Publisher.DeleteFailed");
        }
    }
}
