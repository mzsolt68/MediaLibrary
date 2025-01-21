using Shouldly;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.UnitTests.Books
{
    /// <summary>
    /// Unit tests for <see cref="TagBook"/> class
    /// </summary>
    public class TagBookTests
    {
        /// <summary
        /// Proper parameters should create a new instance of TagBook
        /// <see cref="TagBook.Create(Guid, Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParametersShoulCreateNewTagBook()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var tagId = Guid.NewGuid();
            // Act
            var result = TagBook.Create(bookId, tagId);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.BookID.ShouldBe(bookId);
            result.Value.TagID.ShouldBe(tagId);
            result.Value.IsActive.ShouldBeTrue();
            result.Value.Id.ShouldNotBe(Guid.Empty);
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }

        /// <summary>
        /// Empty book id should return error
        /// <see cref="TagBook.Create(Guid, Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyBookIdShouldReturnError()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            // Act
            var result = TagBook.Create(Guid.Empty, tagId);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("BookID.Missing");
            result.Error.Message.ShouldBe("BookID is required");
        }

        /// <summary>
        /// Empty tag id should return error
        /// <see cref="TagBook.Create(Guid, Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyTagIdShouldReturnError()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            // Act
            var result = TagBook.Create(bookId, Guid.Empty);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Code.ShouldBe("TagID.Missing");
            result.Error.Message.ShouldBe("TagID is required");
        }
    }
}
