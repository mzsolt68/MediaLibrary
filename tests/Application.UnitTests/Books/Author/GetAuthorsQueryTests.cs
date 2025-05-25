using Application.Abstractions.Data;
using Application.Books;
using Application.Dto;
using Domain.Models.Books;
using MockQueryable;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetAuthorsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IAuthorRepository> _authorRepository;
        private readonly SearchParamsDTO _searchParams = new SearchParamsDTO
        {
            PageNumber = 1,
            PageSize = 10,
            SearchParams = []
        };

        public GetAuthorsQueryTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _authorRepository = new Mock<IAuthorRepository>();
            _unitOfWork.Setup(x => x.AuthorRepository).Returns(_authorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfAuthorDTOs_WhenAuthorsExist_NoSearchParams()
        {
            // Arrange
            var authors = new List<Author>
            {
                Author.Create("Lastname1", "Firstname1", "").Value,
                Author.Create("Lastname2", "Firstname2", "Middlename2").Value,
                Author.Create("Lastname3", "Firstname3", "").Value
            }.BuildMock();

            _authorRepository.Setup(x => x.GetAll()).Returns(authors);

            var query = new GetAuthorsQuery(_searchParams);
            var handler = new GetAuthorsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(3);
            result.Value[0].FirstName.ShouldBe("Firstname1");
            result.Value[0].LastName.ShouldBe("Lastname1");
            result.Value[0].MiddleName.ShouldBe("");
            result.Value[1].FirstName.ShouldBe("Firstname2");
            result.Value[1].LastName.ShouldBe("Lastname2");
            result.Value[1].MiddleName.ShouldBe("Middlename2");
            result.Value[2].FirstName.ShouldBe("Firstname3");
            result.Value[2].LastName.ShouldBe("Lastname3");
            result.Value[2].MiddleName.ShouldBe("");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoAuthorsExist()
        {
            var emptyAuthors = new List<Author>().AsQueryable().BuildMock();
            // Arrange
            _authorRepository.Setup(x => x.GetAll()).Returns(emptyAuthors);

            var query = new GetAuthorsQuery(_searchParams);
            var handler = new GetAuthorsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Authors.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

    }
}
