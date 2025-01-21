using Shouldly;
using Domain.Models.Audio;
using SharedKernel;

namespace Domain.UnitTests.Audio
{
    /// <summary>
    /// Unit tests for <see cref="PerformerSong"/> entity.
    /// </summary>
    public class PerformerSongTests
    {
        /// <summary>
        /// Proper parameters should return <see cref="PerformerSong"/>.
        /// </summary>
        [Fact]
        public void ProperParametersShouldReturnPerformerSong()
        {
            // Arrange
            var performerID = Guid.NewGuid();
            var songID = Guid.NewGuid();
            // Act
            var result = PerformerSong.Create(performerID, songID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.PerformerID.ShouldBe(performerID);
            result.Value.SongID.ShouldBe(songID);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty performer ID should return error.
        /// <see cref="PerformerSong.PerformerID"/> is required.
        /// </summary>
        [Fact]
        public void EmptyPerformerIDShouldReturnError()
        {
            // Arrange
            var performerID = Guid.Empty;
            var songID = Guid.NewGuid();
            // Act
            var result = PerformerSong.Create(performerID, songID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PerformerID.Missing");
            result.Error.Message.ShouldBe("Performer ID is missing");
        }

        /// <summary>
        /// Empty song ID should return error.
        /// <see cref="PerformerSong.SongID"/> is required.
        /// </summary>
        [Fact]
        public void EmptySongIDShouldReturnError()
        {
            // Arrange
            var performerID = Guid.NewGuid();
            var songID = Guid.Empty;
            // Act
            var result = PerformerSong.Create(performerID, songID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("SongID.Missing");
            result.Error.Message.ShouldBe("Song ID is missing");
        }
    }
}
