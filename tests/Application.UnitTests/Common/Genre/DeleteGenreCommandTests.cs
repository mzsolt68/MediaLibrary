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
        private readonly Mock<IUnitOfWork> _context;
        private readonly Mock<IGenreRepository> _genres;
        public DeleteGenreCommandTests()
        {
            _context = new Mock<IUnitOfWork>();
            _genres = new Mock<IGenreRepository>();
            _context.Setup(x => x.GenreRepository).Returns(_genres.Object);
        }

        /// <summary>
        /// Proper parameters should set genre to inactive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ProperParametersShouldSetGenreToInactive()
        {
            // Arrange
            _context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);
            var genre = Genre.Create("Test Genre", "Test Type").Value;
            _genres.Setup(x => x.GetByIdAsync(genre.Id)).ReturnsAsync(genre);
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
            _genres.Setup(x => x.GetByIdAsync(genreId)).ReturnsAsync((Genre?)null); 
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
