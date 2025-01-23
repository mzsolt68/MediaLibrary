

using Domain.Models.Audio;
using Domain.Models.Books;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        #region Audio
        DbSet<Album> Albums { get; }
        DbSet<AlbumSong> AlbumSongs { get; }
        DbSet<AudioFormat> AudioFormats { get; }
        DbSet<PerformerSong> PerformerSongs { get; }
        DbSet<Song> Songs { get; }
        DbSet<SongPerformer> SongPerformers { get; }
        #endregion

        #region Book
        DbSet<Author> Authors { get; }
        DbSet<AuthorBook> AuthorBooks { get; }
        DbSet<Book> Books { get; }
        DbSet<BookFormat> BookFormats { get; }
        DbSet<FormatBook> FormatBooks { get; }
        DbSet<Publisher> Publishers { get; }
        DbSet<TagBook> TagBooks { get; }
        #endregion

        #region Common
        DbSet<Genre> Genres { get; }
        DbSet<Tag> Tags { get; }
        DbSet<Language> Languages { get; }
        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
