using Domain.Models.Books;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Persistence.UnitTests;

public class MediaDbContextTests : Testing
{
    [Fact]
    public void DbContext_ShouldHaveRequiredDbSets()
    {
        // Arrange
        using var context = CreateDbContext(nameof(DbContext_ShouldHaveRequiredDbSets));
        
        // Assert
        context.Genres.ShouldNotBeNull();
        context.Tags.ShouldNotBeNull();
        context.Languages.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task SaveChangesAsync_ShouldSetCreatedAtForNewEntities()
    {
        // Arrange
        using var context = CreateDbContext(nameof(SaveChangesAsync_ShouldSetCreatedAtForNewEntities));
        var tag = Tag.Create("Test Tag").Value;
        
        // Act
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
        
        // Assert
        tag.CreatedAt.ShouldNotBe(default);
        tag.UpdatedAt.ShouldBe(default);
    }
    
    [Fact]
    public async Task SaveChangesAsync_ShouldSetUpdatedAtForModifiedEntities()
    {
        // Arrange
        using var context = CreateDbContext(nameof(SaveChangesAsync_ShouldSetUpdatedAtForModifiedEntities));
        var tag = Tag.Create("Original Name").Value;
        
        // Initial save
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
        
        // Act - modify and save again
        tag.Update("Updated Name");
        await context.SaveChangesAsync();
        
        // Assert
        tag.CreatedAt.ShouldNotBe(default);
        tag.UpdatedAt.ShouldNotBe(default);
    }
    
    [Fact]
    public void OnModelCreating_ShouldApplyEntityConfigurations()
    {
        // Arrange
        using var context = CreateDbContext(nameof(OnModelCreating_ShouldApplyEntityConfigurations));
        
        // Act
        var model = context.Model;
        
        // Assert
        var entityTypes = model.GetEntityTypes();
        entityTypes.ShouldNotBeEmpty();
    }
}
