using Application.Abstractions.Data;
using Application.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books.Author
{
    public class UpdateAuthorCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IAuthorRepository> _authorRepository;

        public UpdateAuthorCommandTests()
        {
            _authorRepository = new Mock<IAuthorRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.AuthorRepository).Returns(_authorRepository.Object);
        }

        [Fact]
        public async Task ProperParametersShouldUpdateAuthor()
        {
            // Arrange
            var author = Domain.Models.Books.Author.Create("Lem", "Stanislaw", "");

            _authorRepository.Setup(x => x.GetByIdAsync(author.Value.Id)).ReturnsAsync(author.Value);
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new UpdateAuthorCommand(
                author.Value.Id,
                "John",
                "Doe",
                "A"
            );

            // Act
            var handler = new UpdateAuthorCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task NonExistentAuthorShouldReturnNotFound()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            _authorRepository.Setup(x => x.GetByIdAsync(authorId)).ReturnsAsync(null as Domain.Models.Books.Author);

            var command = new UpdateAuthorCommand(
                authorId,
                "John",
                "Doe",
                "A"
            );

            // Act
            var handler = new UpdateAuthorCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.NotFound);
            result.Error.Code.ShouldBe("Author.NotFound");
        }

        [Fact]
        public async Task SaveChangeFailureShouldReturnConflict()
        {
            // Arrange
            var author = Domain.Models.Books.Author.Create("Lem", "Stanislaw", "");

            _authorRepository.Setup(x => x.GetByIdAsync(author.Value.Id)).ReturnsAsync(author.Value);
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var command = new UpdateAuthorCommand(
                author.Value.Id,
                "John",
                "Doe",
                "A"
            );

            // Act
            var handler = new UpdateAuthorCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Conflict);
            result.Error.Code.ShouldBe("Author.UpdateFailed");
        }
    }
}
