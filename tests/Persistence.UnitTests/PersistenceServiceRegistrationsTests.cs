using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.UnitTests;

public class PersistenceServiceRegistrationsTests
{
    [Fact]
    public void AddPersistenceServices_ShouldRegisterDbContext()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"ConnectionStrings:DefaultConnection", "Server=localhost;Database=TestDb;Trusted_Connection=True;"}
            })
            .Build();

        // Act
        services.AddPersistenceServices(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dbContext = serviceProvider.GetService<MediaDbContext>();
        dbContext.ShouldNotBeNull();
    }

    [Fact]
    public void AddPersistenceServices_ShouldRegisterRepositories()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"ConnectionStrings:DefaultConnection", "Server=localhost;Database=TestDb;Trusted_Connection=True;"}
            })
            .Build();

        // Act
        services.AddPersistenceServices(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        serviceProvider.GetService<IGenericRepository<Domain.Models.Common.Tag>>().ShouldNotBeNull();
        serviceProvider.GetService<IGenreRepository>().ShouldNotBeNull();
        serviceProvider.GetService<IAuthorRepository>().ShouldNotBeNull();
        serviceProvider.GetService<IBookRepository>().ShouldNotBeNull();
        serviceProvider.GetService<IBookFormatRepository>().ShouldNotBeNull();
        serviceProvider.GetService<IPublisherRepository>().ShouldNotBeNull();
        serviceProvider.GetService<ITagRepository>().ShouldNotBeNull();
        serviceProvider.GetService<ILanguageRepository>().ShouldNotBeNull();
    }

    [Fact]
    public void AddPersistenceServices_ShouldRegisterUnitOfWork()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"ConnectionStrings:DefaultConnection", "Server=localhost;Database=TestDb;Trusted_Connection=True;"}
            })
            .Build();

        // Act
        services.AddPersistenceServices(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        unitOfWork.ShouldNotBeNull();
    }
}
