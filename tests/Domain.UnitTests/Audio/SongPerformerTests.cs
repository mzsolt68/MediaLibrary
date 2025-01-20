using Shouldly;
using Domain.Models.Audio;
using SharedKernel;

namespace Domain.UnitTests.Audio
{
    /// <summary>
    /// Unit tests for <see cref="SongPerformer"/> entity.
    /// </summary>
    public class SongPerformerTests
    {
        /// <summary>
        /// Proper parameters should return <see cref="SongPerformer"/>.
        /// </summary>
        [Fact]
        public void ProperParametersShouldReturnSongPerformer()
        {
            // Arrange
            var performerName = "Metallica";
            // Act
            var result = SongPerformer.Create(performerName);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.PerformerName.ShouldBe(performerName);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty performer name should return error.
        /// </summary>
        [Fact]
        public void EmptyPerformerNameShouldReturnError()
        {
            // Arrange
            var performerName = "";
            // Act
            var result = SongPerformer.Create(performerName);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PerformerName.Missing");
            result.Error.Message.ShouldBe("Performer name is missing");
        }

        /// <summary>
        /// Update should change performer name.
        /// </summary>
        [Fact]
        public void UpdateShouldChangePerformerName()
        {
            // Arrange
            var performerName = "Metallica";
            var newPerformerName = "Iron Maiden";
            var songPerformer = SongPerformer.Create(performerName).Value;
            // Act
            var result = songPerformer.Update(newPerformerName);
            var updatedAt = songPerformer.UpdatedAt;
            // Assert
            songPerformer.PerformerName.ShouldBe(newPerformerName);
            songPerformer.UpdatedAt.ShouldBe(updatedAt);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
        }

        /// <summary>
        /// Empty performer name should return error when updating.
        /// <see cref="SongPerformer.Update(string)"/>
        /// </summary>
        [Fact]
        public void EmptyPerformerNameShouldReturnErrorWhenUpdating()
        {
            // Arrange
            var performerName = "Metallica";
            var newPerformerName = "";
            var songPerformer = SongPerformer.Create(performerName).Value;
            // Act
            var result = songPerformer.Update(newPerformerName);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PerformerName.Missing");
            result.Error.Message.ShouldBe("Performer name is missing");
        }
    }
}
