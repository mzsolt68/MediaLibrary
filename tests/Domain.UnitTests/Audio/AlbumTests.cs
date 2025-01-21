using Shouldly;
using Domain.Models.Audio;
using SharedKernel;

namespace Domain.UnitTests.Audio
{
    /// <summary>
    /// Test cases for <see cref="Album"/> entity.
    /// </summary>
    public class AlbumTests
    {
        /// <summary>
        /// Proper parameters should return <see cref="Album"/>.
        /// </summary>
        [Fact]
        public void ProperParametersShouldReturnAlbum()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            // Act
            var result = Album.Create(albumTitle, audioFormatID, nrOfDiscs);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.AlbumTitle.ShouldBe(albumTitle);
            result.Value.AudioFormatID.ShouldBe(audioFormatID);
            result.Value.NrOfDiscs.ShouldBe(nrOfDiscs);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }

        /// <summary>
        /// Empty album title should return error.
        /// <see cref="Album.Create(string, Guid, byte)"/>
        /// </summary>
        [Fact]
        public void EmptyAlbumTitleShouldReturnError()
        {
            // Arrange
            var albumTitle = "";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            // Act
            var result = Album.Create(albumTitle, audioFormatID, nrOfDiscs);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("AlbumTitle.Missing");
            result.Error.Message.ShouldBe("Album title is missing");
        }

        /// <summary>
        /// Empty audio format ID should return error.
        /// <see cref="Album.Create(string, Guid, byte)"/>
        /// </summary>
        [Fact]
        public void EmptyAudioFormatIDShouldReturnError()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.Empty;
            byte nrOfDiscs = 1;
            // Act
            var result = Album.Create(albumTitle, audioFormatID, nrOfDiscs);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("AudioFormatID.Missing");
            result.Error.Message.ShouldBe("Audio format is missing");
        }

        /// <summary>
        /// Zero number of discs should return error.
        /// <see cref="Album.Create(string, Guid, byte)"/>
        /// </summary>
        [Fact]
        public void ZeroNrOfDiscsShouldReturnError()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 0;
            // Act
            var result = Album.Create(albumTitle, audioFormatID, nrOfDiscs);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("NrOfDiscs.Invalid");
            result.Error.Message.ShouldBe("Number of discs is invalid");
        }

        /// <summary>
        /// Update should change album title.
        /// <see cref="Album.UpdateTitle(string)"/>
        /// </summary>
        [Fact]
        public void UpdateShouldChangeAlbumTitle()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var newAlbumTitle = "Ride the Lightning";
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            // Act
            var result = album.UpdateTitle(newAlbumTitle);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            album.AlbumTitle.ShouldBe(newAlbumTitle);
            album.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }

        /// <summary>
        /// Empty album title should return error when updating.
        /// <see cref="Album.UpdateTitle(string)"/>
        /// </summary>
        [Fact]
        public void EmptyAlbumTitleShouldReturnErrorWhenUpdating()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var newAlbumTitle = "";
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            // Act
            var result = album.UpdateTitle(newAlbumTitle);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("AlbumTitle.Missing");
            result.Error.Message.ShouldBe("Album title is missing");
        }

        /// <summary>
        /// Update should change audio format ID.
        /// <see cref="Album.UpdateFormat(Guid)"/>
        /// </summary>
        [Fact]
        public void UpdateShouldChangeAudioFormatID()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var newAudioFormatID = Guid.NewGuid();
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            // Act
            var result = album.UpdateFormat(newAudioFormatID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            album.AudioFormatID.ShouldBe(newAudioFormatID);
            album.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }

        /// <summary>
        /// Empty audio format ID should return error when updating.
        /// <see cref="Album.UpdateFormat(Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyAudioFormatIDShouldReturnErrorWhenUpdating()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var newAudioFormatID = Guid.Empty;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            // Act
            var result = album.UpdateFormat(newAudioFormatID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("AudioFormatID.Missing");
            result.Error.Message.ShouldBe("Audio format is missing");
        }

        /// <summary>
        /// Update should change number of discs.
        /// <see cref="Album.UpdateNrOfDiscs(byte)"/>
        /// </summary>  
        [Fact]
        public void UpdateShouldChangeNrOfDiscs()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            byte newNrOfDiscs = 2;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            // Act
            var result = album.UpdateNrOfDiscs(newNrOfDiscs);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            album.NrOfDiscs.ShouldBe(newNrOfDiscs);
            album.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }

        /// <summary>
        /// Add track should add a new track to the album.
        /// <see cref="Album.AddTrack(Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void AddTrackShouldAddNewTrackToAlbum()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "5:00";
            byte disc = 1;
            var note = "First track";
            // Act
            var result = album.AddTrack(songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<AlbumSong>>();
            album.Tracks.Count.ShouldBe(1);
            album.Tracks.First().SongID.ShouldBe(songID);
            album.Tracks.First().TrackNr.ShouldBe(trackNr);
            album.Tracks.First().PlayTime.ShouldBe(playTime);
            album.Tracks.First().Disc.ShouldBe(disc);
            album.Tracks.First().Note.ShouldBe(note);
            album.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }

        /// <summary>
        /// Empty song ID should return error when adding track.
        /// <see cref="Album.AddTrack(Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptySongIDShouldReturnErrorWhenAddingTrack()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.Empty;
            var trackNr = 1;
            var playTime = "5:00";
            byte disc = 1;
            var note = "First track";
            // Act
            var result = album.AddTrack(songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Song.Missing");
            result.Error.Message.ShouldBe("There's no song to add to the album.");
            album.Tracks.Count.ShouldBe(0);
        }

        /// <summary>
        /// Empty track number should return error when adding track.
        /// <see cref="Album.AddTrack(Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptyTrackNumberShouldReturnErrorWhenAddingTrack()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.NewGuid();
            var trackNr = 0;
            var playTime = "5:00";
            byte disc = 1;
            var note = "First track";
            // Act
            var result = album.AddTrack(songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("TrackNr.Invalid");
            result.Error.Message.ShouldBe("Track number is invalid.");
            album.Tracks.Count.ShouldBe(0);
        }

        /// <summary>
        /// Empty play time should return error when adding track.
        /// <see cref="Album.AddTrack(Guid, int, string, byte, string)"/>
        /// </summary>  
        [Fact]
        public void EmptyPlayTimeShouldReturnErrorWhenAddingTrack()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "";
            byte disc = 1;
            var note = "First track";
            // Act
            var result = album.AddTrack(songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("PlayTime.Missing");
            result.Error.Message.ShouldBe("Play time is missing.");
            album.Tracks.Count.ShouldBe(0);
        }

        /// <summary>
        /// Empty disc should return error when adding track.
        /// <see cref="Album.AddTrack(Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void EmptyDiscShouldReturnErrorWhenAddingTrack()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "5:00";
            byte disc = 0;
            var note = "First track";
            // Act
            var result = album.AddTrack(songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Disc.Invalid");
            result.Error.Message.ShouldBe("Disc number is invalid.");
            album.Tracks.Count.ShouldBe(0);
        }

        /// <summary>
        /// Add track should return error if already added.
        /// <see cref="Album.AddTrack(Guid, int, string, byte, string)"/>
        /// </summary>
        [Fact]
        public void AddTrackShouldReturnErrorIfAlreadyAdded()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "5:00";
            byte disc = 1;
            var note = "First track";
            album.AddTrack(songID, trackNr, playTime, disc, note);
            // Act
            var result = album.AddTrack(songID, trackNr, playTime, disc, note);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Failure);
            result.Error.Code.ShouldBe("Song.AlreadyAdded");
            result.Error.Message.ShouldBe("The song is already added to the album.");
            album.Tracks.Count.ShouldBe(1);
        }

        /// <summary>
        /// Remove track should remove a track from the album.
        /// <see cref="Album.RemoveTrack(Guid)"/>
        /// </summary>
        [Fact]
        public void RemoveTrackShouldRemoveTrackFromAlbum()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.NewGuid();
            var trackNr = 1;
            var playTime = "5:00";
            byte disc = 1;
            var note = "First track";
            album.AddTrack(songID, trackNr, playTime, disc, note);
            // Act
            var result = album.RemoveTrack(songID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            album.Tracks.Count.ShouldBe(0);
            album.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }

        /// <summary>
        /// Remove track should return error if track not found.
        /// <see cref="Album.RemoveTrack(Guid)"/>
        /// </summary>
        [Fact]
        public void RemoveTrackShouldReturnErrorIfTrackNotFound()
        {
            // Arrange
            var albumTitle = "Master of Puppets";
            var audioFormatID = Guid.NewGuid();
            byte nrOfDiscs = 1;
            var album = Album.Create(albumTitle, audioFormatID, nrOfDiscs).Value;
            var songID = Guid.NewGuid();
            // Act
            var result = album.RemoveTrack(songID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.NotFound);
            result.Error.Code.ShouldBe("Song.NotFound");
            result.Error.Message.ShouldBe("The song is not found in the album.");
            album.Tracks.Count.ShouldBe(0);
        }
    }
}
