using Shouldly;
using Moq;
using Application.Common;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Common;
using SharedKernel;

namespace Application.UnitTests.Common
{
    public class UpdateTagCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<DbSet<Tag>> _tags;

        public UpdateTagCommandHandlerTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _tags = new Mock<DbSet<Tag>>();
            _context.Setup(x => x.Tags).Returns(_tags.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateTag_WhenTagExists()
        {
            // Arrange
            var tag = Tag.Create("Old Name").Value;
            _tags.Setup(x => x.FindAsync(tag.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(tag);

            var command = new UpdateTagCommand(tag.Id, "New Name");
            var handler = new UpdateTagCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            tag.TagName.ShouldBe("New Name");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenTagDoesNotExist()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            _tags.Setup(x => x.FindAsync(new object[] { tagId }, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Tag)null);

            var command = new UpdateTagCommand(tagId, "New Name");
            var handler = new UpdateTagCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Tag.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenTagNameIsInvalid()
        {
            // Arrange
            var tag = Tag.Create("Old Name").Value;
            _tags.Setup(x => x.FindAsync(tag.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(tag);

            var command = new UpdateTagCommand(tag.Id, string.Empty);
            var handler = new UpdateTagCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("TagName.Missing");
        }
    }
}
