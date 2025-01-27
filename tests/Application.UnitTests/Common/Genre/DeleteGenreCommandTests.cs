using Application.Abstractions.Data;
using Application.Common;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// Unit tests for the DeleteGenreCommand class.
    /// </summary>
    public class DeleteGenreCommandTests
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<DbSet<Genre>> _genres;
        public DeleteGenreCommandTests()
        {
            _context = new Mock<IApplicationDbContext>();
            _genres = new Mock<DbSet<Genre>>();
            _context.Setup(x => x.Genres).Returns(_genres.Object);
        }

        /// <summary>
        /// Proper parameters should set genre to inactive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ProperParametersShouldSetGenreToInactive()
        {
            // Arrange
            var genre = Genre.Create("Test Genre", "Test Type").Value;
            _genres.Setup(x => x.FindAsync(genre.Id, CancellationToken.None)).ReturnsAsync(genre);
            var command = new DeleteGenreCommand(genre.Id);
            // Act
            var handler = new DeleteGenreCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsSuccess.ShouldBeTrue();
        }

        /// <summary>
        /// Missing genre ID should return failure result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task MissingGenreIdShouldReturnFailureResult()
        {
            // Arrange
            var genreId = Guid.Empty;
            _genres.Setup(x => x.FindAsync(genreId, CancellationToken.None)).ReturnsAsync((Genre)null);
            var command = new DeleteGenreCommand(genreId);
            // Act
            var handler = new DeleteGenreCommandHandler(_context.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Genre.NotFound");
        }
    }
}
