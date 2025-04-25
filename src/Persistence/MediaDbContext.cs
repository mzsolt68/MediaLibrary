using Domain.Models.Books;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    /// <summary>
    /// Represents the database context for the media application.
    /// Provides access to the database entities and configurations.
    /// </summary>
    public class MediaDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public MediaDbContext(DbContextOptions<MediaDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Saves the changes made to the context to the database asynchronously.
        /// Automatically sets the <c>CreatedAt</c> and <c>UpdatedAt</c> properties for tracked entities.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    // Set CreatedAt property
                    entry.CurrentValues["CreatedAt"] = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Set UpdatedAt property
                    entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Configures the model for the database context using the specified <see cref="ModelBuilder"/>.
        /// Applies all entity configurations from the assembly containing this context.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaDbContext).Assembly);
        }

        #region Common entities

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for genres.
        /// </summary>
        public required DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for tags.
        /// </summary>
        public required DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for languages.
        /// </summary>
        public required DbSet<Language> Languages { get; set; }

        #endregion

        #region Book entities

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for authors.
        /// </summary>
        public required DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the association between authors and books.
        /// </summary>
        public required DbSet<AuthorBook> AuthorsOfBooks { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for books.
        /// </summary>
        public required DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for book formats.
        /// </summary>
        public required DbSet<BookFormat> BookFormats { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the association between formats and books.
        /// </summary>
        public required DbSet<FormatBook> FormatsOfBooks { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for publishers.
        /// </summary>
        public required DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the association between tags and books.
        /// </summary>
        public required DbSet<TagBook> TagsOfBooks { get; set; }

        #endregion
    }
}
