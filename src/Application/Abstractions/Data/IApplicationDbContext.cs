

using Domain.Models.Audio;
using Domain.Models.Books;
using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents the application database context interface.
    /// Provides access to the database sets and methods for saving changes.
    /// </summary>
    public interface IApplicationDbContext
    {
        #region Audio

        /// <summary>
        /// Gets the database set of albums.
        /// </summary>
        DbSet<Album> Albums { get; }

        /// <summary>
        /// Gets the database set of album-song relationships.
        /// </summary>
        DbSet<AlbumSong> AlbumSongs { get; }

        /// <summary>
        /// Gets the database set of audio formats.
        /// </summary>
        DbSet<AudioFormat> AudioFormats { get; }

        /// <summary>
        /// Gets the database set of performer-song relationships.
        /// </summary>
        DbSet<PerformerSong> PerformerSongs { get; }

        /// <summary>
        /// Gets the database set of songs.
        /// </summary>
        DbSet<Song> Songs { get; }

        /// <summary>
        /// Gets the database set of song performers.
        /// </summary>
        DbSet<SongPerformer> SongPerformers { get; }

        #endregion

        #region Book

        /// <summary>
        /// Gets the database set of authors.
        /// </summary>
        DbSet<Author> Authors { get; }

        /// <summary>
        /// Gets the database set of author-book relationships.
        /// </summary>
        DbSet<AuthorBook> AuthorBooks { get; }

        /// <summary>
        /// Gets the database set of books.
        /// </summary>
        DbSet<Book> Books { get; }

        /// <summary>
        /// Gets the database set of book formats.
        /// </summary>
        DbSet<BookFormat> BookFormats { get; }

        /// <summary>
        /// Gets the database set of format-book relationships.
        /// </summary>
        DbSet<FormatBook> FormatBooks { get; }

        /// <summary>
        /// Gets the database set of publishers.
        /// </summary>
        DbSet<Publisher> Publishers { get; }

        /// <summary>
        /// Gets the database set of tag-book relationships.
        /// </summary>
        DbSet<TagBook> TagBooks { get; }

        #endregion

        #region Common

        /// <summary>
        /// Gets the database set of genres.
        /// </summary>
        DbSet<Genre> Genres { get; }

        /// <summary>
        /// Gets the database set of tags.
        /// </summary>
        DbSet<Tag> Tags { get; }

        /// <summary>
        /// Gets the database set of languages.
        /// </summary>
        DbSet<Language> Languages { get; }

        #endregion

        /// <summary>
        /// Saves all changes made in this context to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
