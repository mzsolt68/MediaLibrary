using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    /// <summary>
    /// Provides extension methods for registering persistence services in the dependency injection container.
    /// </summary>
    public static class PersistenceServiceRegistrations
    {
        /// <summary>
        /// Adds persistence-related services to the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
        /// <param name="configuration">The application configuration containing connection strings and other settings.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Registers the MediaDbContext with SQL Server as the database provider.
            services.AddDbContext<MediaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registers generic repository for all entity types.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Registers specific repositories for various entities.
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookFormatRepository, BookFormatRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();

            // Registers the Unit of Work pattern for managing transactions.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
