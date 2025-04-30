using Application.Abstractions.Data;
using Application.Common;
using Application.Dto.Common;
using Domain.Models.Common;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace Application.UnitTests.Common
{
    public class GetTagsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITagRepository> _tagRepository;

        public GetTagsQueryHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _tagRepository = new Mock<ITagRepository>();
            _unitOfWork.Setup(x => x.TagRepository).Returns(_tagRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfTagDTOs_WhenTagsExist()
        {
            // Arrange
            var tags = new List<Tag>
                {
                    Tag.Create("Horror").Value,
                    Tag.Create("Comedy").Value
                };
            _tagRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Tag, bool>>>())).ReturnsAsync(tags);

            // Provide a valid predicate for the query
            Expression<Func<Tag, bool>> predicate = tag => true;
            var query = new GetTagsQuery<Tag>(predicate);
            var handler = new GetTagsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Count.ShouldBe(2);
            result.Value.GetType().ShouldBe(typeof(List<TagDTO>));
            result.Value[0].TagName.ShouldBe("Horror");
            result.Value[1].TagName.ShouldBe("Comedy");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoTagsExist()
        {
            // Arrange
            _tagRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Tag, bool>>>())).ReturnsAsync(new List<Tag>());

            // Provide a valid predicate for the query
            Expression<Func<Tag, bool>> predicate = tag => true;
            var query = new GetTagsQuery<Tag>(predicate); // Updated to pass the required parameter
            var handler = new GetTagsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Tags.NotFound");
        }
    }
}
