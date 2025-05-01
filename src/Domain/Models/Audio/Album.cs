using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Models.Audio
{
    /// <summary>
    /// Represents an album entity in the domain.
    /// </summary>
    public class Album : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// It is used for EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private Album(Guid id) : base(id) { }

        private HashSet<AlbumSong> _tracks;

        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the album.</param>
        /// <param name="albumTitle">The title of the album.</param>
        /// <param name="audioFormatID">The unique identifier of the audio format.</param>
        /// <param name="nrOfDiscs">The number of discs in the album.</param>
        private Album(Guid id, string albumTitle, Guid audioFormatID, Byte nrOfDiscs) : base(id)
        {
            AlbumTitle = albumTitle;
            AudioFormatID = audioFormatID;
            NrOfDiscs = nrOfDiscs;
            _tracks = new HashSet<AlbumSong>();
        }

        /// <summary>
        /// Gets the title of the album.
        /// </summary>
        [Required]
        [Display(Name = "Album címe")]
        public string AlbumTitle { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the audio format.
        /// </summary>
        public Guid AudioFormatID { get; private set; }

        /// <summary>
        /// Gets the audio format of the album.
        /// </summary>
        [Display(Name = "Formátum")]
        public AudioFormat AlbumFormat { get; private set; }

        /// <summary>
        /// Gets the number of discs in the album.
        /// </summary>
        [Display(Name = "Lemezek száma")]
        public Byte NrOfDiscs { get; private set; }

        /// <summary>
        /// Gets the collection of tracks in the album.
        /// </summary>
        [Display(Name = "Dalok")]
        public virtual ICollection<AlbumSong> Tracks => _tracks;

        /// <summary>
        /// Gets the number of songs in the album.
        /// </summary>
        [NotMapped]
        public int NrOfSongs => _tracks.Count;

        /// <summary>
        /// Creates a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="albumTitle">The title of the album.</param>
        /// <param name="audioFormatID">The unique identifier of the audio format.</param>
        /// <param name="nrOfDiscs">The number of discs in the album.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created album or an error.</returns>
        public static Result<Album> Create(string albumTitle, Guid audioFormatID, Byte nrOfDiscs)
        {
            if (string.IsNullOrWhiteSpace(albumTitle))
            {
                return Result.Failure<Album>(new Error("AlbumTitle.Missing", "Album title is missing", ErrorType.Validation));
            }
            if (nrOfDiscs < 1)
            {
                return Result.Failure<Album>(new Error("NrOfDiscs.Invalid", "Number of discs is invalid", ErrorType.Validation));
            }
            if (audioFormatID == Guid.Empty)
            {
                return Result.Failure<Album>(new Error("AudioFormatID.Missing", "Audio format is missing", ErrorType.Validation));
            }
            var album = new Album(Guid.NewGuid(), albumTitle, audioFormatID, nrOfDiscs);
            album.IsActive = true;
            return Result.Success(album);
        }

        /// <summary>
        /// Updates the title of the album.
        /// </summary>
        /// <param name="albumTitle">The new title of the album.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        public Result UpdateTitle(string albumTitle)
        {
            if (string.IsNullOrWhiteSpace(albumTitle))
            {
                return Result.Failure(new Error("AlbumTitle.Missing", "Album title is missing", ErrorType.Validation));
            }
            AlbumTitle = albumTitle;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        /// <summary>
        /// Updates the audio format of the album.
        /// </summary>
        /// <param name="audioFormatID">The unique identifier of the new audio format.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        public Result UpdateFormat(Guid audioFormatID)
        {
            if (audioFormatID == Guid.Empty)
            {
                return Result.Failure(new Error("AudioFormatID.Missing", "Audio format is missing", ErrorType.Validation));
            }
            AudioFormatID = audioFormatID;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        /// <summary>
        /// Updates the number of discs in the album.
        /// </summary>
        /// <param name="nrOfDiscs">The new number of discs.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        public Result UpdateNrOfDiscs(Byte nrOfDiscs)
        {
            NrOfDiscs = nrOfDiscs;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }

        /// <summary>
        /// Adds a track to the album.
        /// </summary>
        /// <param name="songID">The unique identifier of the song.</param>
        /// <param name="trackNr">The track number of the song in the album.</param>
        /// <param name="playTime">The playtime of the song.</param>
        /// <param name="disc">The disc number where the song is located.</param>
        /// <param name="note">Additional notes about the song.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the added track or an error.</returns>
        public Result<AlbumSong> AddTrack(Guid songID, int trackNr, string playTime, Byte disc, string note)
        {
            if (_tracks.Any(x => x.SongID == songID))
            {
                return Result.Failure<AlbumSong>(new Error("Song.AlreadyAdded", "The song is already added to the album.", ErrorType.Failure));
            }
            var albumSong = AlbumSong.Create(Id, songID, trackNr, playTime, disc, note);
            if (albumSong.IsFailure)
            {
                return albumSong;
            }
            _tracks.Add(albumSong.Value);
            UpdatedAt = DateTime.UtcNow;
            return Result.Success(albumSong.Value);
        }

        /// <summary>
        /// Removes a track from the album.
        /// </summary>
        /// <param name="songID">The unique identifier of the song to remove.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
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
