using Shouldly;
using Moq;
using Application.Common.Genre.Create;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Common;
using SharedKernel;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the CreateGenreCommand class.
    /// </summary>
    public class CreateGenreCommandTests
    {
        private readonly Mock<IApplicationDbContext> _context;

        public CreateGenreCommandTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _context.Setup(x=> x.Genres).Returns(new Mock<DbSet<Genre>>().Object);
        }

        /// <summary>
        /// Proper parameters should create a new genre
        /// <see cref="CreateGenreCommand"/>
        /// </summary>
        [Fact]
        public async Task ProperParametersShouldCreateNewGenre()
        {
            // Arrange
            var genreName = "Test Genre";
            var genreType = "Test Type";
            var command = new CreateGenreCommand
            {
                GenreName = genreName,
                GenreType = genreType
            };
            // Act
            var handler = new CreateGenreCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<Guid>();
        }

        /// <summary>
        /// Missing genre name should return a failure result
        /// <see cref="CreateGenreCommand"/>
        /// </summary>
        [Fact]
        public async Task MissingGenreNameShouldReturnFailureResult()
        {
            // Arrange
            var genreName = string.Empty;
            var genreType = "Test Type";
            var command = new CreateGenreCommand
            {
                GenreName = genreName,
                GenreType = genreType
            };
            // Act
            var handler = new CreateGenreCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("GenreName.Missing");
        }

        /// <summary>
        /// missing genre type should return a failure result
        /// <see cref="CreateGenreCommand"/>
        /// </summary>
        [Fact]
        public async Task MissingGenreTypeShouldReturnFailureResult()
        {
            // Arrange
            var genreName = "Test Genre";
            var genreType = string.Empty;
            var command = new CreateGenreCommand
            {
                GenreName = genreName,
                GenreType = genreType
            };
            // Act
            var handler = new CreateGenreCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Type.ShouldBe(ErrorType.Validation);
            result.Error.Code.ShouldBe("GenreType.Missing");
        }
    }
}
