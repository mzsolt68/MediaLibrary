using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Entities.Models.Books;
using MediaLibrary.Entities.Models.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Entities.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<AudioFormat> AudioFormats { get; set; }
        public DbSet<SongPerformer> SongPerformers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<PerformerSong> PerformerSongs { get; set; }
        public DbSet<AlbumSong> AlbumSongs { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookFormat> BookFormats { get; set; }
        public DbSet<TagBook> TagBooks { get; set; }
        public DbSet<FormatBook> FormatBooks { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            TableConnections.AudioTableConnections(builder);
            TableConnections.BookTableConnections(builder);
            FillAudioTestData.Fill(builder);
        }
    }
}
