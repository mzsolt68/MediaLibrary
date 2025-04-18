using Application.Abstractions.Data;
using Application.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books.Book
{
    public class DeleteBookCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteBookCommandHandler _handler;
        public DeleteBookCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteBookCommandHandler(_unitOfWorkMock.Object);
        }
        [Fact]
        public async Task Handle_BookNotFound_ReturnsNotFoundError()
        {
            // Arrange
            var command = new DeleteBookCommand(Guid.NewGuid());
            _unitOfWorkMock.Setup(x => x.BookRepository.GetByIdAsync(command.BookId))
                .ReturnsAsync((Domain.Models.Books.Book?)null);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Book.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }
        [Fact]
        public async Task Handle_DeleteFailed_ReturnsConflictError()
        {
            // Arrange
            var book = Domain.Models.Books.Book.Create(
                "Test Book",
                "First Edition",
                Guid.NewGuid(),
                "2023",
                "1234567890",
                Guid.NewGuid()
            ).Value;
            var command = new DeleteBookCommand(book.Id);
            _unitOfWorkMock.Setup(x => x.BookRepository.GetByIdAsync(command.BookId))
                .ReturnsAsync(book);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Book.DeleteFailed");
            result.Error.Type.ShouldBe(ErrorType.Conflict);
        }
        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var command = new DeleteBookCommand(Guid.NewGuid());
            var book = Domain.Models.Books.Book.Create(
                "Test Book",
                "First Edition",
                Guid.NewGuid(),
                "2023",
                "1234567890",
                Guid.NewGuid()
            ).Value;
            _unitOfWorkMock.Setup(x => x.BookRepository.GetByIdAsync(command.BookId))
                .ReturnsAsync(book);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
