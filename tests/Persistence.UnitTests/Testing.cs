using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitTests;

/// <summary>
/// Base class for testing that provides common functionality and setup for persistence tests
/// </summary>
public abstract class Testing
{
    /// <summary>
    /// Creates a new in-memory database instance for testing
    /// </summary>
    /// <param name="databaseName">Name for the in-memory database</param>
    /// <returns>A configured MediaDbContext using an in-memory database</returns>
    protected static MediaDbContext CreateDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<MediaDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
            
        return new MediaDbContext(options);
    }
}
