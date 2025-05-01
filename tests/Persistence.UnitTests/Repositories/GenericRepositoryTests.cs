using Application.Abstractions.Data;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Shouldly;
using System.Linq.Expressions;

namespace Persistence.UnitTests.Repositories;

public class GenericRepositoryTests : Testing
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(GetAllAsync_ShouldReturnAllEntities));
        var repository = new GenericRepository<Tag>(dbContext);
        
        var tags = new List<Tag>
        {
            Tag.Create("Tag1").Value,
            Tag.Create("Tag2").Value,
            Tag.Create("Tag3").Value
        };
        
        dbContext.Tags.AddRange(tags);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await repository.GetAllAsync(_ => true);
        
        // Assert
        result.Count().ShouldBe(3);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectEntity()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(GetByIdAsync_ShouldReturnCorrectEntity));
        var repository = new GenericRepository<Tag>(dbContext);
        
        var tag = Tag.Create("TargetTag").Value;
        dbContext.Tags.Add(tag);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await repository.GetByIdAsync(tag.Id);
        
        // Assert
        result.ShouldNotBeNull();
        result.TagName.ShouldBe("TargetTag");
    }
    
    [Fact]
    public async Task AddAsync_ShouldAddEntityToContext()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(AddAsync_ShouldAddEntityToContext));
        var repository = new GenericRepository<Tag>(dbContext);
        
        var tag = Tag.Create("NewTag").Value;
        
        // Act
        await repository.AddAsync(tag);
        
        // Assert
        var entry = dbContext.Entry(tag);
        entry.State.ShouldBe(EntityState.Added);
    }
    
    [Fact]
    public void Update_ShouldUpdateEntityInContext()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(Update_ShouldUpdateEntityInContext));
        var repository = new GenericRepository<Tag>(dbContext);
        
        var tag = Tag.Create("OriginalName").Value;
        dbContext.Tags.Add(tag);
        dbContext.SaveChanges();
        
        // Detach to simulate fetching from DB
        dbContext.Entry(tag).State = EntityState.Detached;
        
        // Act
        tag.Update("UpdatedName");
        repository.UpdateAsync(tag);
        
        // Assert
        var entry = dbContext.Entry(tag);
        entry.State.ShouldBe(EntityState.Modified);
    }
    
    [Fact]
    public void Remove_ShouldRemoveEntityFromContext()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(Remove_ShouldRemoveEntityFromContext));
        var repository = new GenericRepository<Tag>(dbContext);
        
        var tag = Tag.Create("ToDelete").Value;
        dbContext.Tags.Add(tag);
        dbContext.SaveChanges();
        
        // Act
        repository.DeleteAsync(tag);
        
        // Assert
        var entry = dbContext.Entry(tag);
        entry.State.ShouldBe(EntityState.Deleted);
    }
    
    //[Fact]
    //public async Task FindAsync_ShouldReturnMatchingEntities()
    //{
    //    // Arrange
    //    var dbContext = CreateDbContext(nameof(FindAsync_ShouldReturnMatchingEntities));
    //    var repository = new GenericRepository<Tag>(dbContext);
        
    //    var tags = new List<Tag>
    //    {
    //        Tag.Create("Action").Value,
    //        Tag.Create("Drama").Value,
    //        Tag.Create("Action Adventure").Value
    //    };
        
    //    dbContext.Tags.AddRange(tags);
    //    await dbContext.SaveChangesAsync();
        
    //    // Act
    //    Expression<Func<Tag, bool>> filter = t => t.Name.Contains("Action");
    //    var result = await repository.FindAsync(filter);
        
    //    // Assert
    //    result.Count().ShouldBe(2);
    //    result.All(t => t.Name.Contains("Action")).ShouldBeTrue();
    //}
}
