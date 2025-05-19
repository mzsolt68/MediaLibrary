using Domain.Models.Common;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.UnitTests.Repositories;

public class TagRepositoryTests : Testing
{
    [Fact]
    public async Task TagRepository_ShouldInheritGenericRepositoryBehavior()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(TagRepository_ShouldInheritGenericRepositoryBehavior));
        var repository = new TagRepository(dbContext);
        
        var tag = Tag.Create("Fantasy").Value;
        
        // Act
        repository.Add(tag);
        await dbContext.SaveChangesAsync();
        
        // Assert
        var savedTag = await repository.GetByIdAsync(tag.Id);
        savedTag.ShouldNotBeNull();
        savedTag.TagName.ShouldBe("Fantasy");
    }
    
    [Fact]
    public async Task TagRepository_ShouldHandleMultipleTags()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(TagRepository_ShouldHandleMultipleTags));
        var repository = new TagRepository(dbContext);

        var tags = new List<Tag>
        {
            Tag.Create("Fantasy").Value,
            Tag.Create("Sci-Fi").Value,
            Tag.Create("Mystery").Value
        };

        // Act
        foreach (var tag in tags)
        {
            repository.Add(tag);
        }
        await dbContext.SaveChangesAsync();

        // Assert
        var allTags = repository.GetAll(_ => true);
        allTags.Count().ShouldBe(3);
    }
    
    //[Fact]
    //public async Task TagRepository_ShouldFindTagsByName()
    //{
    //    // Arrange
    //    var dbContext = CreateDbContext(nameof(TagRepository_ShouldFindTagsByName));
    //    var repository = new TagRepository(dbContext);
        
    //    var tags = new List<Tag>
    //    {
    //        Tag.Create("Fantasy").Value,
    //        Tag.Create("Dark Fantasy").Value,
    //        Tag.Create("Sci-Fi").Value
    //    };
        
    //    foreach (var tag in tags)
    //    {
    //        await repository.AddAsync(tag);
    //    }
    //    await dbContext.SaveChangesAsync();
        
    //    // Act
    //    var result = await repository.FindAsync(t => t.Name.Contains("Fantasy"));
        
    //    // Assert
    //    result.Count().ShouldBe(2);
    //    result.All(t => t.Name.Contains("Fantasy")).ShouldBeTrue();
    //}
}
