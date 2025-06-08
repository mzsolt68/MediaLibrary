using Application.Abstractions.Data;
using Application.Books;
using Application.Dto.Books;
using Domain.Models.Books;
using Moq;
using SharedKernel;
using Shouldly;

namespace Application.UnitTests.Books
{
    public class GetPublisherByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IPublisherRepository> _publisherRepository;

        public GetPublisherByIdQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _publisherRepository = new Mock<IPublisherRepository>();
            _unitOfWork.Setup(x => x.PublisherRepository).Returns(_publisherRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPublisherDTO_WhenPublisherExists()
        {
            // Arrange
            var publisher = Publisher.Create("Test Publisher").Value;
            _publisherRepository.Setup(x => x.GetByIdAsync(publisher.Id, CancellationToken.None))
                .ReturnsAsync(publisher);

            var query = new GetPublisherByIdQuery(publisher.Id);
            var handler = new GetPublisherByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.IsFailure.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeOfType<BookPublisherDTO>();
            result.Value.PublisherName.ShouldBe("Test Publisher");
            result.Value.PublisherID.ShouldBe(publisher.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenPublisherDoesNotExist()
        {
            // Arrange
            var publisherId = Guid.NewGuid();
            _publisherRepository.Setup(x => x.GetByIdAsync(publisherId, CancellationToken.None))
                .ReturnsAsync((Publisher?)null);

            var query = new GetPublisherByIdQuery(publisherId);
            var handler = new GetPublisherByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.IsSuccess.ShouldBeFalse();
            result.Error.Type.ShouldBe(ErrorType.NotFound);
            result.Error.Code.ShouldBe("Publisher.NotFound");
            result.Error.Message.ShouldBe("The publisher was not found in the database.");
        }
    }
}
