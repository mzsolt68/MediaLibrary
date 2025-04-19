using Application.Abstractions.Data;
using Application.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class DeleteAuthorCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteAuthorCommandHandler _handler;

        public DeleteAuthorCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteAuthorCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_AuthorNotFound_ReturnsNotFoundError()
        {
            // Arrange
            var command = new DeleteAuthorCommand(Guid.NewGuid());
            _unitOfWorkMock.Setup(x => x.AuthorRepository.GetByIdAsync(command.bookId))
                .ReturnsAsync((Domain.Models.Books.Author?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Author.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Handle_DeleteFailed_ReturnsProblemError()
        {
            // Arrange
            var author = Domain.Models.Books.Author.Create("John", "Doe", "").Value;
            var command = new DeleteAuthorCommand(author.Id);

            _unitOfWorkMock.Setup(x => x.AuthorRepository.GetByIdAsync(command.bookId))
                .ReturnsAsync(author);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Author.DeleteFailed");
            result.Error.Type.ShouldBe(ErrorType.Problem);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var author = Domain.Models.Books.Author.Create("John", "Doe", "").Value;
            var command = new DeleteAuthorCommand(author.Id);

            _unitOfWorkMock.Setup(x => x.AuthorRepository.GetByIdAsync(command.bookId))
                .ReturnsAsync(author);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
