using Domain.Models.Common;
using SharedKernel;
using Shouldly;


namespace Domain.UnitTests.Common
{
    public class GenreTests
    {
        [Fact]
        public void ProperParametersShouldReturnGenre()
        {
            // Arrange
            var genreName = "Horror";
            var genreType = "Scary";
            // Act
            var result = Genre.Create(genreName, genreType);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
            result.Value.GenreName.ShouldBe(genreName);
            result.Value.GenreType.ShouldBe(genreType);
            result.Value.ShouldNotBeNull();
            result.Value.IsActive.ShouldBeTrue();
            result.Value.CreatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.Value.Id.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public void EmptyGenreNameShouldReturnError()
        {
            // Arrange
            var genreName = "";
            var genreType = "Scary";
            // Act
            var result = Genre.Create(genreName, genreType);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("GenreName.Missing");
            result.Error.Message.ShouldBe("Genre name is missing");
        }

        [Fact]
        public void EmptyGenreTypeShouldReturnError()
        {
            // Arrange
            var genreName = "Horror";
            var genreType = "";
            // Act
            var result = Genre.Create(genreName, genreType);
            // Assert
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("GenreType.Missing");
            result.Error.Message.ShouldBe("Genre type is missing");
        }

        [Fact]
        public void UpdateShouldChangeGenreNameAndGenreType()
        {
            // Arrange
            var genreName = "Horror";
            var genreType = "Scary";
            var genre = Genre.Create(genreName, genreType).Value;
            var newGenreName = "Comedy";
            var newGenreType = "Funny";
            // Act
            var result = genre.Update(newGenreName, newGenreType);
            // Assert
            genre.ShouldNotBeNull();
            genre.IsActive.ShouldBeTrue();
            genre.GenreName.ShouldBe(newGenreName);
            genre.GenreType.ShouldBe(newGenreType);
            genre.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeFalse();
            result.IsSuccess.ShouldBeTrue();
        }

        /// <summary>
        /// Empty genre name should return error when updating
        /// <see cref="Genre"/>
        /// </summary>
        [Fact]
        public void UpdateEmptyGenreNameShouldReturnError()
        {
            // Arrange
            var genreName = "Horror";
            var genreType = "Scary";
            var genre = Genre.Create(genreName, genreType).Value;
            var newGenreName = "";
            var newGenreType = "Funny";
            // Act
            var result = genre.Update(newGenreName, newGenreType);
            // Assert
            genre.ShouldNotBeNull();
            genre.IsActive.ShouldBeTrue();
            genre.GenreName.ShouldBe(genreName);
            genre.GenreType.ShouldBe(genreType);
            genre.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("GenreName.Missing");
            result.Error.Message.ShouldBe("Genre name is missing");
        }

        /// <summary>
        /// Empty genre type should return error when updating
        /// <see cref="Genre"/>
        /// </summary>
        [Fact]
        public void UpdateEmptyGenreTypeShouldReturnError()
        {
            // Arrange
            var genreName = "Horror";
            var genreType = "Scary";
            var genre = Genre.Create(genreName, genreType).Value;
            var newGenreName = "Comedy";
            var newGenreType = "";
            // Act
            var result = genre.Update(newGenreName, newGenreType);
            // Assert
            genre.ShouldNotBeNull();
            genre.IsActive.ShouldBeTrue();
            genre.GenreName.ShouldBe(genreName);
            genre.GenreType.ShouldBe(genreType);
            genre.UpdatedAt.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            result.ShouldNotBeNull();
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("GenreType.Missing");
            result.Error.Message.ShouldBe("Genre type is missing");
        }
    }
}
