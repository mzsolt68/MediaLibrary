using Shouldly;
using Domain.Models.Audio;
using SharedKernel;

namespace Domain.UnitTests.Audio
{
    /// <summary>
    /// Unit tests for <see cref="AlbumSong"/> entity.
    /// </summary>
    public class AlbumSongTests
    {
        /// <summary>
        /// Proper parameters should return <see cref="AlbumSong"/>.
        /// </summary>
        [Fact]
        public void ProperParametersShouldReturnAlbumSong()
        {
            // Arrange
            var albumID = Guid.NewGuid();
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "3:45";
            byte disc = 1;
            var note = "Note";
            // Act
            var result = AlbumSong.Create(albumID, songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.AlbumID.ShouldBe(albumID);
            result.Value.SongID.ShouldBe(songID);
            result.Value.TrackNr.ShouldBe(trackNr);
            result.Value.PlayTime.ShouldBe(playTime);
            result.Value.Disc.ShouldBe(disc);
            result.Value.Note.ShouldBe(note);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }
        /// <summary>
        /// Empty play time should return error.
        /// <see cref="AlbumSong.Create(Guid, Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptyPlayTimeShouldReturnError()
        {
            // Arrange
            var albumID = Guid.NewGuid();
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "";
            byte disc = 1;
            var note = "Note";
            // Act
            var result = AlbumSong.Create(albumID, songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PlayTime.Missing");
            result.Error.Message.ShouldBe("Play time is missing");
        }

        /// <summary>
        /// Empty album ID should return error.
        /// <see cref="AlbumSong.Create(Guid, Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptyAlbumIDShouldReturnError()
        {
            // Arrange
            var albumID = Guid.Empty;
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "3:45";
            byte disc = 1;
            var note = "Note";
            // Act
            var result = AlbumSong.Create(albumID, songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("AlbumID.Missing");
            result.Error.Message.ShouldBe("Album ID is missing");
        }

        /// <summary>
        /// Empty song ID should return error.
        /// <see cref="AlbumSong.Create(Guid, Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptySongIDShouldReturnError()
        {
            // Arrange
            var albumID = Guid.NewGuid();
            var songID = Guid.Empty;
            var trackNr = 1;
            var playTime = "3:45";
            byte disc = 1;
            var note = "Note";
            // Act
            var result = AlbumSong.Create(albumID, songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("SongID.Missing");
            result.Error.Message.ShouldBe("Song ID is missing");
        }

        /// <summary>   
        /// Empty track number should return error.
        /// <see cref="AlbumSong.Create(Guid, Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptyTrackNumberShouldReturnError()
        {
            // Arrange
            var albumID = Guid.NewGuid();
            var songID = Guid.NewGuid();
            var trackNr = 0;
            var playTime = "3:45";
            byte disc = 1;
            var note = "Note";
            // Act
            var result = AlbumSong.Create(albumID, songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("TrackNr.Invalid");
            result.Error.Message.ShouldBe("Track number is invalid");
        }

        /// <summary>
        /// Empty disc should return error.
        /// <see cref="AlbumSong.Create(Guid, Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptyDiscShouldReturnError()
        {
            // Arrange
            var albumID = Guid.NewGuid();
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "3:45";
            byte disc = 0;
            var note = "Note";
            // Act
            var result = AlbumSong.Create(albumID, songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Disc.Invalid");
            result.Error.Message.ShouldBe("Disc number is invalid");
        }
    }
}
