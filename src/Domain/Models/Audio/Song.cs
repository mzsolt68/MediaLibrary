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

        public static Song Create(string songTitle, string songLyric, Guid genreID, Guid languageID)
        {
            var song = new Song(Guid.NewGuid(), songTitle, songLyric, genreID, languageID);
            song.IsActive = true;
            return song;
        }

        public void UpdateTitle(string songTitle)
        {
            SongTitle = songTitle;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateLyric(string songLyric)
        {
            SongLyric = songLyric;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateGenre(Guid genreID)
        {
            GenreID = genreID;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateLanguage(Guid languageID)
        {
            LanguageID = languageID;
            UpdatedAt = DateTime.UtcNow;
        }

        public Result<PerformerSong> AddPerformer(Guid performerID)
        {
            if (_performers.Any(p => p.PerformerID == performerID))
            {
                Result.Failure<PerformerSong>(new Error("Performer.Exists", "Performer already added to song.", ErrorType.Failure));
            }
            var performerSong = PerformerSong.Create(performerID, Id);
            _performers.Add(performerSong);
            return Result.Success(performerSong);
        }

        public Result<PerformerSong> RemovePerformer(Guid performerID)
        {
            var performerSong = _performers.FirstOrDefault(p => p.PerformerID == performerID);
            if (performerSong == null)
            {
                Result.Failure<PerformerSong>(new Error("Performer.NotFound", "Performer not found in song.", ErrorType.NotFound));
            }
            _performers.Remove(performerSong);
            return Result.Success(performerSong);
        }
    }
}
