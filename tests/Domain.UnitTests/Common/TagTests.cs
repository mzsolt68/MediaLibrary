using Domain.Models.Common;
using SharedKernel;
using Shouldly;

namespace Domain.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the <see cref="Tag"/> entity.
    /// </summary>
    public class TagTests
    {
        /// <summary>
        /// Tests the <see cref="Tag.Create(string)"/> method.
        /// </summary>
        [Fact]
        public void ProperParametersShouldReturnTag()
        {
            // Arrange
            var tagName = "Horror";
            // Act
            var result = Tag.Create(tagName);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.TagName.ShouldBe(tagName);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }
        /// <summary>
        /// Tests the <see cref="Tag.Create(string)"/> method with an empty tag name.
        /// </summary>
        [Fact]
        public void EmptyTagNameShouldReturnError()
        {
            // Arrange
            var tagName = "";
            // Act
            var result = Tag.Create(tagName);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("TagName.Missing");
            result.Error.Message.ShouldBe("Tag name is missing");
        }
        /// <summary>
        /// Tests the <see cref="Tag.Update(string)"/> method.
        /// </summary>
        [Fact]
        public void UpdateShouldChangeTagName()
        {
            // Arrange
            var tagName = "Horror";
            var tag = Tag.Create(tagName).Value;
            var newTagName = "Scary";
            // Act
            var result = tag.Update(newTagName);
            // Assert
            tag.TagName.ShouldBe(newTagName);
            tag.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
        }

        /// <summary>
        /// Empty tag name should return error when updating.
        /// <see cref="Tag.Update(string)"/>
        /// </summary>
        [Fact]
        public void UpdateEmptyTagNameShouldReturnError()
        {
            // Arrange
            var tagName = "Horror";
            var tag = Tag.Create(tagName).Value;
            var newTagName = "";
            // Act
            var result = tag.Update(newTagName);
            // Assert
            tag.TagName.ShouldBe(tagName);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("TagName.Missing");
            result.Error.Message.ShouldBe("Tag name is missing");
        }

        /// <summary>
        /// Tests the <see cref="Tag.Inactivate"/> method.
        /// </summary>
        [Fact]
        public void InactivateShouldSetIsActiveToFalse()
        {
            // Arrange
            var tagName = "Horror";
            var tag = Tag.Create(tagName).Value;

            // Act
            tag.Inactivate();

            // Assert
            tag.ShouldNotBeNull();
            tag.IsActive.ShouldBeFalse();
            tag.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }
    }
}
