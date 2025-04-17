using Application.Common;
using Application.Abstractions.Data;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the DeleteTagCommandHandler class.
    /// </summary>
    public class DeleteTagCommandTests
    {
        private readonly Mock<IUnitOfWork> _context;
        private readonly Mock<ITagRepository> _tags;

        public DeleteTagCommandTests()
        {
            _context = new Mock<IUnitOfWork>();
            _tags = new Mock<ITagRepository>();
            _context.Setup(x => x.TagRepository).Returns(_tags.Object);
        }

        /// <summary>
        /// Proper parameters should set tag to inactive.
        /// </summary>
        [Fact]
        public async Task ProperParametersShouldSetTagToInactive()
        {
            // Arrange
            var tag = Tag.Create("Horror").Value;
            var command = new DeleteTagCommand(tag.Id);
            _tags.Setup(x => x.GetByIdAsync(tag.Id)).ReturnsAsync(tag);
            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var handler = new DeleteTagCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            tag.IsActive.ShouldBeFalse();
        }

        /// <summary>
        /// Missing tag ID should return failure result.
        /// </summary>
        [Fact]
        public async Task MissingTagIdShouldReturnFailureResult()
        {
            // Arrange
            var tagId = Guid.Empty;
            _tags.Setup(x => x.GetByIdAsync(tagId)).ReturnsAsync((Tag?)null);
            var command = new DeleteTagCommand(tagId);

            // Act
            var handler = new DeleteTagCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Tag.NotFound");
        }
    }
}
