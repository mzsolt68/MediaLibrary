using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Models.Audio
{
    public class Album : Entity
    {
        private HashSet<AlbumSong> _tracks;

        private Album(Guid id, string albumTitle, Guid audioFormatID, Byte nrOfDiscs) : base(id)
        {
            AlbumTitle = albumTitle;
            AudioFormatID = audioFormatID;
            NrOfDiscs = nrOfDiscs;
            _tracks = new HashSet<AlbumSong>();
        }

        [Required]
        [Display(Name = "Album címe")]
        public string AlbumTitle { get; private set; }
        public Guid AudioFormatID { get; private set; }
        [Display(Name = "Formátum")]
        public AudioFormat AlbumFormat { get; private set; }
        [Display(Name = "Lemezek száma")]
        public Byte NrOfDiscs { get; private set; }

        [Display(Name = "Dalok")]
        public virtual ICollection<AlbumSong> Tracks => _tracks;

        [NotMapped]
        public int NrOfSongs => _tracks.Count;

        public static Result<Album> Create(string albumTitle, Guid audioFormatID, Byte nrOfDiscs)
        {
            if(string.IsNullOrWhiteSpace(albumTitle))
            {
                return Result.Failure<Album>(new Error("AlbumTitle.Missing", "Album title is missing", ErrorType.Validation));
            }
            if(nrOfDiscs < 1)
            {
                return Result.Failure<Album>(new Error("NrOfDiscs.Invalid", "Number of discs is invalid", ErrorType.Validation));
            }
            if(audioFormatID == Guid.Empty)
            {
                return Result.Failure<Album>(new Error("AudioFormatID.Missing", "Audio format is missing", ErrorType.Validation));
            }
            var album = new Album(Guid.NewGuid(), albumTitle, audioFormatID, nrOfDiscs);
            album.IsActive = true;
            return Result.Success(album);
        }

        public Result UpdateTitle(string albumTitle)
        {
            if(string.IsNullOrWhiteSpace(albumTitle))
            {
                return Result.Failure(new Error("AlbumTitle.Missing", "Album title is missing", ErrorType.Validation));
            }
            AlbumTitle = albumTitle;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        public Result UpdateFormat(Guid audioFormatID)
        {
            if(audioFormatID == Guid.Empty)
            {
                return Result.Failure(new Error("AudioFormatID.Missing", "Audio format is missing", ErrorType.Validation));
            }
            AudioFormatID = audioFormatID;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        public Result UpdateNrOfDiscs(Byte nrOfDiscs)
        {
            NrOfDiscs = nrOfDiscs;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        public Result<AlbumSong> AddTrack(Guid songID, int trackNr, string playTime, Byte disc, string note)
        {
            if(_tracks.Any(x => x.SongID == songID))
            {
                return Result.Failure<AlbumSong>(new Error("Song.AlreadyAdded", "The song is already added to the album.", ErrorType.Failure));
            }
            var albumSong = AlbumSong.Create(Id, songID, trackNr, playTime, disc, note);
            _tracks.Add(albumSong);
            UpdatedAt = DateTime.UtcNow;
            return Result.Success(albumSong);
        }

        public Result RemoveTrack(Guid songID)
        {
            var albumSong = _tracks.FirstOrDefault(x => x.SongID == songID);
            if (albumSong == null)
            {
                return Result.Failure(new Error("Song.NotFound", "The song is not found in the album.", ErrorType.NotFound));
            }
            _tracks.Remove(albumSong);
            UpdatedAt = DateTime.UtcNow;
            return Result.Success(albumSong);
        }
    }
}
