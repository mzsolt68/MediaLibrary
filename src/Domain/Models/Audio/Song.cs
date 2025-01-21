using Domain.Models.Common;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Audio
{
    public class Song : Entity
    {
        private HashSet<PerformerSong> _performers;
        private HashSet<AlbumSong> _albums;

        private Song(Guid id, string songTitle, string songLyric, Guid genreID, Guid languageID) : base(id)
        {
            SongTitle = songTitle;
            SongLyric = songLyric;
            GenreID = genreID;
            LanguageID = languageID;
            _performers = new HashSet<PerformerSong>();
            _albums = new HashSet<AlbumSong>();
        }

        [Required]
        [Display(Name = "Zeneszám címe")]
        public string SongTitle { get; set; }
        [Display(Name = "Szöveg")]
        [DataType(DataType.MultilineText)]
        public string SongLyric { get; set; }
        public Guid GenreID { get; set; }
        [Display(Name = "Műfaj")]
        public Genre Genre { get; set; }
        public Guid LanguageID { get; set; }
        [Display(Name = "Nyelv")]
        public Language Language { get; set; }

        public virtual ICollection<PerformerSong> Performers => _performers.ToList();
        public virtual ICollection<AlbumSong> Albums => _albums.ToList();

        public static Result<Song> Create(string songTitle, string songLyric, Guid genreID, Guid languageID)
        {
            if(string.IsNullOrWhiteSpace(songTitle))
            {
                return Result.Failure<Song>(new Error("SongTitle.Missing", "Song title is missing", ErrorType.Validation));
            }
            if(genreID == Guid.Empty)
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

        public Result UpdateTitle(string songTitle)
        {
            if(string.IsNullOrWhiteSpace(songTitle))
            {
                return Result.Failure(new Error("SongTitle.Missing", "Song title is missing", ErrorType.Validation));
            }
            SongTitle = songTitle;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        public void UpdateLyric(string songLyric)
        {
            SongLyric = songLyric;
            UpdatedAt = DateTime.UtcNow;
        }

        public Result UpdateGenre(Guid genreID)
        {
            if(genreID == Guid.Empty)
            {
                return Result.Failure(new Error("Genre.Missing", "Genre is missing", ErrorType.Validation));
            }
            GenreID = genreID;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        public Result UpdateLanguage(Guid languageID)
        {
            if(languageID == Guid.Empty)
            {
                return Result.Failure(new Error("Language.Missing", "Language is missing", ErrorType.Validation));
            }
            LanguageID = languageID;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        public Result<PerformerSong> AddPerformer(Guid performerID)
        {
            if(performerID == Guid.Empty)
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

        public Result<PerformerSong> RemovePerformer(Guid performerID)
        {
            if(performerID == Guid.Empty)
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
