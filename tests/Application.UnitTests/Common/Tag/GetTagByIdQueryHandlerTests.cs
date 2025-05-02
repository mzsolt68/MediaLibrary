using Application.Abstractions.Data;
using Application.Common;
using Application.Dto.Common;
using Domain.Models.Common;
using Moq;
using Shouldly;

namespace Application.UnitTests.Common
{
    public class GetTagByIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITagRepository> _tagRepository;

        public GetTagByIdQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _tagRepository = new Mock<ITagRepository>();
            _unitOfWork.Setup(x => x.TagRepository).Returns(_tagRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTagDTO_WhenTagExists()
        {
            // Arrange
            var tag = Tag.Create("Horror").Value;
            _tagRepository.Setup(x => x.GetByIdAsync(tag.Id, CancellationToken.None)).ReturnsAsync(tag);

            var query = new GetTagByIdQuery(tag.Id);
            var handler = new GetTagByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.GetType().ShouldBe(typeof(TagDTO));
            result.Value.TagName.ShouldBe("Horror");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenTagDoesNotExist()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            _tagRepository.Setup(x => x.GetByIdAsync(tagId, CancellationToken.None)).ReturnsAsync((Tag?)null);

            var query = new GetTagByIdQuery(tagId);
            var handler = new GetTagByIdQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Tag.NotFound");
        }
    }
}
