using Shouldly;
using Domain.Models.Audio;
using SharedKernel;

namespace Domain.UnitTests.Audio
{
    /// <summary>
    /// Unit tests for the <see cref="AudioFormat"/> entity.
    /// </summary>
    public class AudioFormatTests
    {
        [Fact]
        public void ProperParameterShouldReturnAudioFormat()
        {
            // Arrange
            var audioFormatName = "MP3";
            // Act
            var result = AudioFormat.Create(audioFormatName);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.AudioFormatName.ShouldBe(audioFormatName);
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }
        [Fact]
        public void UpdateShouldChangeAudioFormatName()
        {
            // Arrange
            var audioFormat = AudioFormat.Create("MP3").Value;
            var newAudioFormatName = "WAV";
            // Act
            var result = audioFormat.UpdateName(newAudioFormatName);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            audioFormat.AudioFormatName.ShouldBe(newAudioFormatName);
        }
        [Fact]
        public void SetActiveStateShouldSetNewState()
        {
            // Arrange
            var audioFormat = AudioFormat.Create("MP3").Value;
            var newState = false;
            // Act
            var result = audioFormat.SetActiveState(newState);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            audioFormat.IsActive.ShouldBe(newState);
        }

        /// <summary>
        /// Empty audio format name should return error when creating a new audio format.
        /// <see cref="AudioFormat.Create(string)"/>
        /// </summary>
        [Fact]
        public void EmptyAudioFormatNameShouldReturnError()
        {
            // Arrange
            var audioFormatName = "";
            // Act
            var result = AudioFormat.Create(audioFormatName);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("AudioFormatName.Missing");
            result.Error.Message.ShouldBe("Audio format name is missing");
        }

        /// <summary>
        /// Empty audio format name should return error when updating.
        /// <see cref="AudioFormat.UpdateName(string)"/>
        /// </summary>
        [Fact]
        public void EmptyAudioFormatNameShouldReturnErrorWhenUpdating()
        {
            // Arrange
            var audioFormat = AudioFormat.Create("MP3").Value;
            var newAudioFormatName = "";
            // Act
            var result = audioFormat.UpdateName(newAudioFormatName);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("AudioFormatName.Missing");
            result.Error.Message.ShouldBe("Audio format name is missing");
        }
    }
}
