using Domain.Models.Books;
using Domain.Models.Common;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.UnitTests;

public class UnitOfWorkTests : Testing
{
    [Fact]
    public void UnitOfWork_ShouldProvideAccessToAllRepositories()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(UnitOfWork_ShouldProvideAccessToAllRepositories));
        var unitOfWork = new UnitOfWork(dbContext);
        
        // Assert
        unitOfWork.GenreRepository.ShouldNotBeNull();
        unitOfWork.AuthorRepository.ShouldNotBeNull();
        unitOfWork.BookRepository.ShouldNotBeNull();
        unitOfWork.BookFormatRepository.ShouldNotBeNull();
        unitOfWork.PublisherRepository.ShouldNotBeNull();
        unitOfWork.TagRepository.ShouldNotBeNull();
        unitOfWork.LanguageRepository.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task SaveChangesAsync_ShouldPersistChanges()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(SaveChangesAsync_ShouldPersistChanges));
        var unitOfWork = new UnitOfWork(dbContext);
        
        var tag = Tag.Create("Test Tag").Value;
        await unitOfWork.TagRepository.AddAsync(tag);
        
        // Act
        var result = await unitOfWork.SaveChangesAsync();
        
        // Assert
        result.ShouldBe(1); // Should save 1 entity
        
        // Verify the entity was saved
        var savedTag = await unitOfWork.TagRepository.GetByIdAsync(tag.Id);
        savedTag.ShouldNotBeNull();
        savedTag.Id.ShouldBe(tag.Id);
    }
    
    [Fact]
    public async Task UnitOfWork_ShouldAllowOperationsOnMultipleRepositories()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(UnitOfWork_ShouldAllowOperationsOnMultipleRepositories));
        var unitOfWork = new UnitOfWork(dbContext);
        
        var tag = Tag.Create("Fantasy").Value;
        var bookFormat = BookFormat.Create("Paperback").Value;      
        // Act
        await unitOfWork.TagRepository.AddAsync(tag);
        await unitOfWork.BookFormatRepository.AddAsync(bookFormat);
        var result = await unitOfWork.SaveChangesAsync();
        
        // Assert
        result.ShouldBe(2); // Should save 2 entities
        
        // Verify both entities were saved
        var savedTag = await unitOfWork.TagRepository.GetByIdAsync(tag.Id);
        var savedFormat = await unitOfWork.BookFormatRepository.GetByIdAsync(bookFormat.Id);
        
        savedTag.ShouldNotBeNull();
        savedFormat.ShouldNotBeNull();
    }
}
