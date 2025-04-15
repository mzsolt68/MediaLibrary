using Shouldly;
using Moq;
using Application.Common;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Common;
using SharedKernel;

namespace Application.UnitTests.Common
{
    public class UpdateGenreCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<DbSet<Genre>> _genres;

        public UpdateGenreCommandHandlerTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _genres = new Mock<DbSet<Genre>>();
            _context.Setup(x => x.Genres).Returns(_genres.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateGenre_WhenGenreExists()
        {
            // Arrange
            var genre = Genre.Create("Old Name", "Old Type").Value;
            _genres.Setup(x => x.FindAsync(genre.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(genre);

            var command = new UpdateGenreCommand(genre.Id, "New Name", "New Type");
            var handler = new UpdateGenreCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            genre.GenreName.ShouldBe("New Name");
            genre.GenreType.ShouldBe("New Type");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenGenreDoesNotExist()
        {
            // Arrange
            var genreId = Guid.NewGuid();
            _genres.Setup(x => x.FindAsync(new object[] { genreId }, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Genre)null);

            var command = new UpdateGenreCommand(genreId, "New Name", "New Type");
            var handler = new UpdateGenreCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Genre.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenGenreNameIsInvalid()
        {
            // Arrange
            var genre = Genre.Create("Old Name", "Old Type").Value;
            _genres.Setup(x => x.FindAsync(genre.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(genre);

            var command = new UpdateGenreCommand(genre.Id, string.Empty, "New Type");
            var handler = new UpdateGenreCommandHandler(_context.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("GenreName.Missing");
        }
    }
}
