using Application.Books;
using Application.Abstractions.Data;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class CreateAuthorCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateAuthorCommandTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.AuthorRepository).Returns(new Mock<IAuthorRepository>().Object);
        }

        [Fact]
        public async Task ProperParametersShouldCreateNewAuthor()
        {
            // Arrange
            var command = new CreateAuthorCommand
            {
                LastName = "Doe",
                FirstName = "John",
                MiddleName = "A"
            };

            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new CreateAuthorCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task MissingLastNameShouldReturnFailure()
        {
            // Arrange
            var command = new CreateAuthorCommand
            {
                LastName = string.Empty, // Invalid
                FirstName = "John",
                MiddleName = "A"
            };

            // Act
            var handler = new CreateAuthorCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Author.Lastname.Required");
        }
    }
}
