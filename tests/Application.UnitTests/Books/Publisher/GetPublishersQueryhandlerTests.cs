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
    public class GetPublishersQueryhandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IPublisherRepository> _publisherRepository;
        private readonly SearchParamsDTO _searchParams = new SearchParamsDTO
        {
            PageNumber = 1,
            PageSize = 10,
            SearchParams = []
        };

        public GetPublishersQueryhandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _publisherRepository = new Mock<IPublisherRepository>();
            _unitOfWork.Setup(x => x.PublisherRepository).Returns(_publisherRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfPublisherDTOs_WhenPublishersExist_NoSearchParams()
        {
            // Arrange
            var publishers = new List<Publisher>
            {
                Publisher.Create("Publisher1").Value,
                Publisher.Create("Publisher2").Value,
                Publisher.Create("Publisher3").Value
            }.BuildMock();

            _publisherRepository.Setup(x => x.GetAll()).Returns(publishers);

            var query = new GetPublishersQuery(_searchParams);
            var handler = new GetPublishersQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(3);
            result.Value[0].PublisherName.ShouldBe("Publisher1");
            result.Value[1].PublisherName.ShouldBe("Publisher2");
            result.Value[2].PublisherName.ShouldBe("Publisher3");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoPublishersExist()
        {
            // Arrange
            var emptyPublishers = new List<Publisher>().AsQueryable().BuildMock();
            _publisherRepository.Setup(x => x.GetAll()).Returns(emptyPublishers);

            var query = new GetPublishersQuery(_searchParams);
            var handler = new GetPublishersQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Publishers.NotFound");
            result.Error.Type.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFilteredResults_WhenSearchParamsProvided()
        {
            // Arrange
            var publishers = new List<Publisher>
            {
                Publisher.Create("Publisher1").Value,
                Publisher.Create("Publisher2").Value,
                Publisher.Create("Publisher3").Value
            }.BuildMock();

            _publisherRepository
                .Setup(x => x.GetAll(It.IsAny<System.Linq.Expressions.Expression<System.Func<Publisher, bool>>>()))
                .Returns(publishers);

            var searchParams = new SearchParamsDTO
            {
                PageNumber = 1,
                PageSize = 10,
                SearchParams = [new SearchParam { PropertyName = "PublisherName", Value = "Publisher" }]
            };

            var query = new GetPublishersQuery(searchParams);
            var handler = new GetPublishersQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_ShouldReturnPaginatedResults()
        {
            // Arrange
            var publishers = new List<Publisher>
            {
                Publisher.Create("Publisher1").Value,
                Publisher.Create("Publisher2").Value,
                Publisher.Create("Publisher3").Value,
                Publisher.Create("Publisher4").Value,
                Publisher.Create("Publisher5").Value
            }.BuildMock();

            _publisherRepository.Setup(x => x.GetAll()).Returns(publishers);

            var searchParams = new SearchParamsDTO
            {
                PageNumber = 2,
                PageSize = 2,
                SearchParams = []
            };

            var query = new GetPublishersQuery(searchParams);
            var handler = new GetPublishersQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value[0].PublisherName.ShouldBe("Publisher3");
            result.Value[1].PublisherName.ShouldBe("Publisher4");
        }
    }
}
