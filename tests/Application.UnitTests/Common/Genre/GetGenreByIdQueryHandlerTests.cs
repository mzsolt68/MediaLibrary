using Application.Abstractions.Data;
using Application.Common;
using Application.Dto.Common;
using Domain.Models.Common;
using Moq;
using Shouldly;

namespace Application.UnitTests.Common
{
    public class GetGenreByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IGenreRepository> _genreRepository;

        public GetGenreByIdQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _genreRepository = new Mock<IGenreRepository>();
            _unitOfWork.Setup(x => x.GenreRepository).Returns(_genreRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnGenreDTO_WhenGenreExists()
        {
            // Arrange
            var genre = Genre.Create("Test Genre", "Test Type").Value;
            _genreRepository.Setup(x => x.GetByIdAsync(genre.Id)).ReturnsAsync(genre);

            var query = new GetGenreByIdQuery { GenreId = genre.Id };
            var handler = new GetGenreByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.GetType().ShouldBe(typeof(GenreDTO));
            result.Value.GenreName.ShouldBe("Test Genre");
            result.Value.GenreType.ShouldBe("Test Type");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenGenreDoesNotExist()
        {
            // Arrange
            var genreId = Guid.NewGuid();
            _genreRepository.Setup(x => x.GetByIdAsync(genreId)).ReturnsAsync((Genre?)null);

            var query = new GetGenreByIdQuery{ GenreId = genreId };
            var handler = new GetGenreByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Genre.NotFound");
        }
    }
}
