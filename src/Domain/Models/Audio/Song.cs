using Domain.Models.Common;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Audio
{
    /// <summary>
    /// Represents a song entity in the domain.
    /// </summary>
    public class Song : Entity
    {
        private HashSet<PerformerSong> _performers;
        private HashSet<AlbumSong> _albums;

        /// <summary>
        /// Initializes a new instance of the <see cref="Song"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the song.</param>
        /// <param name="songTitle">The title of the song.</param>
        /// <param name="songLyric">The lyrics of the song.</param>
        /// <param name="genreID">The unique identifier of the genre.</param>
        /// <param name="languageID">The unique identifier of the language.</param>
        private Song(Guid id, string songTitle, string songLyric, Guid genreID, Guid languageID) : base(id)
        {
            SongTitle = songTitle;
            SongLyric = songLyric;
            GenreID = genreID;
            LanguageID = languageID;
            _performers = new HashSet<PerformerSong>();
            _albums = new HashSet<AlbumSong>();
        }

        /// <summary>
        /// Gets or sets the title of the song.
        /// </summary>
        [Required]
        [Display(Name = "Zeneszám címe")]
        public string SongTitle { get; set; }

        /// <summary>
        /// Gets or sets the lyrics of the song.
        /// </summary>
        [Display(Name = "Szöveg")]
        [DataType(DataType.MultilineText)]
        public string SongLyric { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the genre.
        /// </summary>
        public Guid GenreID { get; set; }

        /// <summary>
        /// Gets or sets the genre of the song.
        /// </summary>
        [Display(Name = "Műfaj")]
        public Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the language.
        /// </summary>
        public Guid LanguageID { get; set; }

        /// <summary>
        /// Gets or sets the language of the song.
        /// </summary>
        [Display(Name = "Nyelv")]
        public Language Language { get; set; }

        /// <summary>
        /// Gets the collection of performers associated with the song.
        /// </summary>
        public virtual ICollection<PerformerSong> Performers => _performers.ToList();

        /// <summary>
        /// Gets the collection of albums associated with the song.
        /// </summary>
        public virtual ICollection<AlbumSong> Albums => _albums.ToList();

        /// <summary>
        /// Creates a new instance of the <see cref="Song"/> class.
        /// </summary>
        /// <param name="songTitle">The title of the song.</param>
        /// <param name="songLyric">The lyrics of the song.</param>
        /// <param name="genreID">The unique identifier of the genre.</param>
        /// <param name="languageID">The unique identifier of the language.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created song or an error.</returns>
        public static Result<Song> Create(string songTitle, string songLyric, Guid genreID, Guid languageID)
        {
            if (string.IsNullOrWhiteSpace(songTitle))
            {
                return Result.Failure<Song>(new Error("SongTitle.Missing", "Song title is missing", ErrorType.Validation));
            }
            if (genreID == Guid.Empty)
            {
                return Result.Failure<Song>(new Error("Genre.Missing", "Genre is missing", ErrorType.Validation));
            }
            if (languageID == Guid.Empty)
            {
                return Result.Failure<Song>(new Error("Language.Missing", "Language is missing", ErrorType.Validation));
            }
            var song = new Song(Guid.NewGuid(), songTitle, songLyric, genreID, languageID);
            song.IsActive = true;
            return Result.Success(song);
        }

        /// <summary>
        /// Updates the title of the song.
        /// </summary>
        /// <param name="songTitle">The new title of the song.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        public Result UpdateTitle(string songTitle)
        {
            if (string.IsNullOrWhiteSpace(songTitle))
            {
                return Result.Failure(new Error("SongTitle.Missing", "Song title is missing", ErrorType.Validation));
            }
            SongTitle = songTitle;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        /// <summary>
        /// Updates the lyrics of the song.
        /// </summary>
        /// <param name="songLyric">The new lyrics of the song.</param>
        public void UpdateLyric(string songLyric)
        {
            SongLyric = songLyric;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the genre of the song.
        /// </summary>
        /// <param name="genreID">The unique identifier of the new genre.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        public Result UpdateGenre(Guid genreID)
        {
            if (genreID == Guid.Empty)
            {
                return Result.Failure(new Error("Genre.Missing", "Genre is missing", ErrorType.Validation));
            }
            GenreID = genreID;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        /// <summary>
        /// Updates the language of the song.
        /// </summary>
        /// <param name="languageID">The unique identifier of the new language.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        public Result UpdateLanguage(Guid languageID)
        {
            if (languageID == Guid.Empty)
            {
                return Result.Failure(new Error("Language.Missing", "Language is missing", ErrorType.Validation));
            }
            LanguageID = languageID;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        /// <summary>
        /// Adds a performer to the song.
        /// </summary>
        /// <param name="performerID">The unique identifier of the performer.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the added performer or an error.</returns>
        public Result<PerformerSong> AddPerformer(Guid performerID)
        {
            if (performerID == Guid.Empty)
            {
                return Result.Failure<PerformerSong>(new Error("Performer.Missing", "Performer is missing", ErrorType.Validation));
            }
            if (Performers.Any(p => p.PerformerID == performerID))
            {
                return Result.Failure<PerformerSong>(new Error("Performer.Exists", "Performer already added to song.", ErrorType.Failure));
            }
            var performerSong = PerformerSong.Create(performerID, Id);
            _performers.Add(performerSong.Value);
            return Result.Success(performerSong.Value);
        }

        /// <summary>
        /// Removes a performer from the song.
        /// </summary>
        /// <param name="performerID">The unique identifier of the performer.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the removed performer or an error.</returns>
        public Result<PerformerSong> RemovePerformer(Guid performerID)
        {
            if (performerID == Guid.Empty)
            {
                return Result.Failure<PerformerSong>(new Error("Performer.Missing", "Performer is missing", ErrorType.Validation));
            }
            var performerSong = Performers.FirstOrDefault(p => p.PerformerID == performerID);
            if (performerSong == null)
            {
                return Result.Failure<PerformerSong>(new Error("Performer.NotFound", "Performer not found in song.", ErrorType.NotFound));
            }
            _performers.Remove(performerSong);
            return Result.Success(performerSong);
        }
    }
}
