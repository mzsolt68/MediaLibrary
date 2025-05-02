using Domain.Models.Books;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.UnitTests.Repositories;

public class BookFormatRepositoryTests : Testing
{
    [Fact]
    public async Task BookFormatRepository_ShouldInheritGenericRepositoryBehavior()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(BookFormatRepository_ShouldInheritGenericRepositoryBehavior));
        var repository = new BookFormatRepository(dbContext);
        
        var bookFormat = BookFormat.Create("Paperback").Value;
        
        // Act
        repository.Add(bookFormat);
        await dbContext.SaveChangesAsync();
        
        // Assert
        var savedFormat = await repository.GetByIdAsync(bookFormat.Id);
        savedFormat.ShouldNotBeNull();
        savedFormat.BookFormatName.ShouldBe("Paperback");
    }
    
    [Fact]
    public async Task BookFormatRepository_ShouldHandleMultipleFormats()
    {
        // Arrange
        var dbContext = CreateDbContext(nameof(BookFormatRepository_ShouldHandleMultipleFormats));
        var repository = new BookFormatRepository(dbContext);
        
        var formats = new List<BookFormat>
        {
            BookFormat.Create("Hardcover").Value,
            BookFormat.Create("E-Book").Value,
            BookFormat.Create("Audiobook").Value
        };
        
        // Act
        foreach (var format in formats)
        {
            repository.Add(format);
        }
        await dbContext.SaveChangesAsync();
        
        // Assert
        var allFormats = await repository.GetAllAsync(_ => true);
        allFormats.Count().ShouldBe(3);
    }
}
