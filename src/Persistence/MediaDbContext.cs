using Domain.Models.Books;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    internal class MediaDbContext : DbContext
    {
        public MediaDbContext(DbContextOptions<MediaDbContext> options)
            : base(options)
        {
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaDbContext).Assembly);
        }

        #region Common entities
        public required DbSet<Genre> Genres { get; set; }
        public required DbSet<Tag> Tags { get; set; }
        public required DbSet<Language> Languages { get; set; }
        #endregion

        #region Book entities
        public required DbSet<Author> Authors { get; set; }
        public required DbSet<AuthorBook> AuthorsOfBooks { get; set; }
        public required DbSet<Book> Books { get; set; }
        public required DbSet<BookFormat> BookFormats { get; set; }
        public required DbSet<FormatBook> FormatsOfBooks { get; set; }
        public required DbSet<Publisher> Publishers { get; set; }
        public required DbSet<TagBook> TagsOfBooks { get; set; }
        #endregion
    }
}
