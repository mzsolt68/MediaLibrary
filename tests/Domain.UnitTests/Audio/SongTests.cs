using Shouldly;
using Domain.Models.Audio;
using SharedKernel;

namespace Domain.UnitTests.Audio
{
    /// <summary>
    /// Unit tests for <see cref="Song"/> entity.
    /// </summary>
    public class SongTests
    {
        /// <summary>
        /// Proper parameters should return song.
        /// <see cref="Song.Create(string, string, Guid, Guid)"/>
        /// </summary>
        [Fact]
        public void ProperParametersShouldReturnSong()
        {
            // Arrange
            var songTitle = "Test song";
            var songLyric = "Test lyric";
            var genreID = Guid.NewGuid();
            var languageID = Guid.NewGuid();
            // Act
            var result = Song.Create(songTitle, songLyric, genreID, languageID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.SongTitle.ShouldBe(songTitle);
            result.Value.SongLyric.ShouldBe(songLyric);
            result.Value.GenreID.ShouldBe(genreID);
            result.Value.LanguageID.ShouldBe(languageID);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
            result.Value.Id.ShouldBeOfType<Guid>();
        }

        /// <summary>
        /// Empty song title should return error.
        /// <see cref="Song.Create(string, string, Guid, Guid)"/>
        /// </summary>
        [Fact]
        public void EmptySongTitleShouldReturnError()
        {
            // Arrange
            var songTitle = "";
            var songLyric = "Test lyric";
            var genreID = Guid.NewGuid();
            var languageID = Guid.NewGuid();
            // Act
            var result = Song.Create(songTitle, songLyric, genreID, languageID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("SongTitle.Missing");
            result.Error.Message.ShouldBe("Song title is missing");
        }

        /// <summary>
        /// Empty GenreID should return error.
        /// <see cref="Song.Create(string, string, Guid, Guid)"/>"
        /// </summary>
        [Fact]
        public void EmptyGenreIDShouldReturnError()
        {
            // Arrange
            var songTitle = "Test song";
            var songLyric = "Test lyric";
            var genreID = Guid.Empty;
            var languageID = Guid.NewGuid();
            // Act
            var result = Song.Create(songTitle, songLyric, genreID, languageID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Genre.Missing");
            result.Error.Message.ShouldBe("Genre is missing");
        }

        /// <summary>
        /// Empty LanguageID should return error.
        /// <see cref="Song.Create(string, string, Guid, Guid)"/>
        /// </summary>
        [Fact]
        public void EmptyLanguageIDShouldReturnError()
        {
            // Arrange
            var songTitle = "Test song";
            var songLyric = "Test lyric";
            var genreID = Guid.NewGuid();
            var languageID = Guid.Empty;
            // Act
            var result = Song.Create(songTitle, songLyric, genreID, languageID);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Language.Missing");
            result.Error.Message.ShouldBe("Language is missing");
        }

        /// <summary>
        /// Add performer should return performer song.
        /// <see cref="Song.AddPerformer(Guid)"/>
        /// </summary>
        [Fact]
        public void AddPerformerShouldReturnPerformerSong()
        {
            // Arrange
            var song = Song.Create("Test song", "Test lyric", Guid.NewGuid(), Guid.NewGuid());
            var performer = SongPerformer.Create("SongPerformer");
            // Act
            var result = song.Value.AddPerformer(performer.Value);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.PerformerID.ShouldBe(performer.Value.Id);
            result.Value.SongID.ShouldBe(song.Value.Id);
            song.Value.Performers.Count.ShouldNotBe(0);
            song.Value.Performers.ShouldContain(performer.Value);
        }

        /// <summary>
        /// Remove performer should return performer song.
        /// <see cref="Song.RemovePerformer(Guid)"/>
        /// </summary>
        [Fact]
        public void RemovePerformerShouldReturnSongPerformer()
        {
            // Arrange
            var song = Song.Create("Test song", "Test lyric", Guid.NewGuid(), Guid.NewGuid());
            var performer = SongPerformer.Create("SongPerformer");
            song.Value.AddPerformer(performer.Value);
            // Act
            var result = song.Value.RemovePerformer(performer.Value);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Id.ShouldBe(performer.Value.Id);
            song.Value.Performers.Count.ShouldBe(0);
            song.Value.Performers.ShouldNotContain(performer.Value);
        }

        /// <summary>
        /// Add performer that already exists should return error.
        /// <see cref="Song.AddPerformer(Guid)"/>
        /// </summary>
        [Fact]
        public void AddPerformerThatAlreadyExistsShouldReturnError()
        {
            // Arrange
            var song = Song.Create("Test song", "Test lyric", Guid.NewGuid(), Guid.NewGuid());
            var performer = SongPerformer.Create("SongPerformer");
            song.Value.AddPerformer(performer.Value);
            // Act
            var result = song.Value.AddPerformer(performer.Value);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Failure);
            result.Error.Code.ShouldBe("Performer.Exists");
            result.Error.Message.ShouldBe("Performer already added to song.");
        }

        /// <summary>
        /// Remove performer that does not exist should return error.
        /// <see cref="Song.RemovePerformer(Guid)"/>"
        /// </summary>
        [Fact]
        public void RemovePerformerThatDoesNotExistShouldReturnError()
        {
            // Arrange
            var song = Song.Create("Test song", "Test lyric", Guid.NewGuid(), Guid.NewGuid());
            var performer = SongPerformer.Create("SongPerformer");
            // Act
            var result = song.Value.RemovePerformer(performer.Value);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.NotFound);
            result.Error.Code.ShouldBe("Performer.NotFound");
            result.Error.Message.ShouldBe("Performer not found in song.");
        }

        /// <summary>
        /// Empty song title should return error when updating song title.
        /// <see cref="Song.UpdateTitle(string)"/>"
        /// </summary>
        [Fact]
        public void EmptySongTitleShouldReturnErrorWhenUpdatingSongTitle()
        {
            // Arrange
            var song = Song.Create("Test song", "Test lyric", Guid.NewGuid(), Guid.NewGuid());
            var songTitle = "";
            // Act
            var result = song.Value.UpdateTitle(songTitle);
            // Assert
            song.Value.SongTitle.ShouldNotBe(songTitle);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("SongTitle.Missing");
            result.Error.Message.ShouldBe("Song title is missing");
        }

        /// <summary>
        /// Empty genre ID should return error when updating genre ID.
        /// <see cref="Song.UpdateGenre(Guid)"/>"
        /// </summary>
        [Fact]
        public void EmptyGenreIDShouldReturnErrorWhenUpdatingGenreID()
        {
            // Arrange
            var song = Song.Create("Test song", "Test lyric", Guid.NewGuid(), Guid.NewGuid());
            var genreID = Guid.Empty;
            // Act
            var result = song.Value.UpdateGenre(genreID);
            // Assert
            song.Value.GenreID.ShouldNotBe(genreID);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Genre.Missing");
            result.Error.Message.ShouldBe("Genre is missing");
        }

        /// <summary>
        /// Empty language ID should return error when updating language ID.
        /// <see cref="Song.UpdateLanguage(Guid)"/>"
        /// </summary>
        [Fact]
        public void EmptyLanguageIDShouldReturnErrorWhenUpdatingLanguageID()
        {
            // Arrange
            var song = Song.Create("Test song", "Test lyric", Guid.NewGuid(), Guid.NewGuid());
            var languageID = Guid.Empty;
            // Act
            var result = song.Value.UpdateLanguage(languageID);
            // Assert
            song.Value.LanguageID.ShouldNotBe(languageID);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("Language.Missing");
            result.Error.Message.ShouldBe("Language is missing");
        }
    }
}
