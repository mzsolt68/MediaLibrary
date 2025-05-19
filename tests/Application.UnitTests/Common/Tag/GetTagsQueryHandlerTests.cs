using Application.Abstractions.Data;
using Application.Common;
using Application.Dto;
using Application.Dto.Common;
using Domain.Models.Common;
using MockQueryable;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace Application.UnitTests.Common
{
    public class GetTagsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITagRepository> _tagRepository;
        private readonly SearchParamsDTO _searchParams = new SearchParamsDTO
        {
            PageNumber = 1,
            PageSize = 10,
            SearchParams = []
        };

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
                }.BuildMock();
            _tagRepository.Setup(x => x.GetAll()).Returns(tags);

            var query = new GetTagsQuery(_searchParams);
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
            var emptyTags = new List<Tag>().BuildMock();
            _tagRepository.Setup(x => x.GetAll()).Returns(emptyTags);

            var query = new GetTagsQuery(_searchParams);
            var handler = new GetTagsQueryHandler(_unitOfWork.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
            result.Error.Code.ShouldBe("Tags.NotFound");
        }
    }
}
