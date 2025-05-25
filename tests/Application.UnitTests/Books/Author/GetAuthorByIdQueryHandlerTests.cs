using Application.Abstractions.Data;
using Application.Books;
using Application.Dto.Books;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetAuthorByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IAuthorRepository> _authorRepository;

        public GetAuthorByIdQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _authorRepository = new Mock<IAuthorRepository>();
            _unitOfWork.Setup(x => x.AuthorRepository).Returns(_authorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthorDTO_WhenAuthorExists()
        {
            // Arrange
            var author = Author.Create("Lastname", "Firstname", "").Value;
            _authorRepository.Setup(x => x.GetByIdAsync(author.Id, CancellationToken.None)).ReturnsAsync(author);

            var query = new GetAuthorByIdQuery(author.Id);
            var handler = new GetAuthorByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.GetType().ShouldBe(typeof(BookAuthorDTO));
            result.Value.LastName.ShouldBe("Lastname");
            result.Value.FirstName.ShouldBe("Firstname");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            _authorRepository.Setup(x => x.GetByIdAsync(authorId, CancellationToken.None)).ReturnsAsync((Author?)null);

            var query = new GetAuthorByIdQuery(authorId);
            var handler = new GetAuthorByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Author.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }
    }
}
