using Shouldly;
using Moq;
using Application.Common;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Common;
using SharedKernel;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the CreateTagCommandHandler class.
    /// </summary>
    public class CreateTagCommandTests
    {
        private readonly Mock<IUnitOfWork> _context;

        public CreateTagCommandTests()
        {
            _context = new Mock<IUnitOfWork>();
            _context.Setup(x => x.TagRepository).Returns(new Mock<ITagRepository>().Object);
        }

        /// <summary>
        /// Proper parameters should create a new tag.
        /// <see cref="CreateTagCommand"/>
        /// </summary>
        [Fact]
        public async Task ProperParametersShouldCreateNewTag()
        {
            // Arrange
            var tagName = "Horror";
            var command = new CreateTagCommand
            {
                TagName = tagName
            };
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new CreateTagCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<Guid>();
        }

        /// <summary>
        /// Missing tag name should return a failure result.
        /// <see cref="CreateTagCommand"/>
        /// </summary>
        [Fact]
        public async Task MissingTagNameShouldReturnFailureResult()
        {
            // Arrange
            var tagName = string.Empty; // Invalid name
            var command = new CreateTagCommand
            {
                TagName = tagName
            };

            // Act
            var handler = new CreateTagCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("TagName.Missing");
        }
    }
}
