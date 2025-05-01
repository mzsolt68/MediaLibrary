using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.Audio
{
    /// <summary>
    /// Represents the association between an album and a song.
    /// </summary>
    public class AlbumSong : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumSong"/> class.
        /// It is used for EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private AlbumSong(Guid id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumSong"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the album-song association.</param>
        /// <param name="albumID">The unique identifier of the album.</param>
        /// <param name="songID">The unique identifier of the song.</param>
        /// <param name="trackNr">The track number of the song in the album.</param>
        /// <param name="playTime">The playtime of the song.</param>
        /// <param name="disc">The disc number where the song is located.</param>
        /// <param name="note">Additional notes about the song.</param>
        private AlbumSong(Guid id, Guid albumID, Guid songID, int trackNr, string playTime, Byte disc, string note) : base(id)
        {
            AlbumID = albumID;
            SongID = songID;
            TrackNr = trackNr;
            PlayTime = playTime;
            Disc = disc;
            Note = note;
        }

        /// <summary>
        /// Gets the unique identifier of the album.
        /// </summary>
        [Required]
        public Guid AlbumID { get; private set; }

        /// <summary>
        /// Gets the album associated with this album-song relationship.
        /// </summary>
        [JsonIgnore]
        public Album Album { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the song.
        /// </summary>
        [Required]
        public Guid SongID { get; private set; }

        /// <summary>
        /// Gets the song associated with this album-song relationship.
        /// </summary>
        public Song Song { get; private set; }

        /// <summary>
        /// Gets the track number of the song in the album.
        /// </summary>
        [Required]
        public int TrackNr { get; private set; }

        /// <summary>
        /// Gets the playtime of the song.
        /// </summary>
        [Required]
        [Display(Name = "Játékidő")]
        public string PlayTime { get; private set; }

        /// <summary>
        /// Gets the disc number where the song is located.
        /// </summary>
        [Display(Name = "Lemez")]
        public Byte Disc { get; private set; }

        /// <summary>
        /// Gets additional notes about the song.
        /// </summary>
        [Display(Name = "Megjegyzés")]
        public string Note { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AlbumSong"/> class.
        /// </summary>
        /// <param name="albumID">The unique identifier of the album.</param>
        /// <param name="songID">The unique identifier of the song.</param>
        /// <param name="trackNr">The track number of the song in the album.</param>
        /// <param name="playTime">The playtime of the song.</param>
        /// <param name="disc">The disc number where the song is located.</param>
        /// <param name="note">Additional notes about the song.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="AlbumSong"/> instance
        /// or an error if validation fails.
        /// </returns>
        public static Result<AlbumSong> Create(Guid albumID, Guid songID, int trackNr, string playTime, Byte disc, string note)
        {
            if (string.IsNullOrWhiteSpace(playTime))
            {
                return Result.Failure<AlbumSong>(new Error("PlayTime.Missing", "Play time is missing", ErrorType.Validation));
            }
            if (albumID == Guid.Empty)
            {
                return Result.Failure<AlbumSong>(new Error("AlbumID.Missing", "Album ID is missing", ErrorType.Validation));
            }
            if (songID == Guid.Empty)
            {
                return Result.Failure<AlbumSong>(new Error("SongID.Missing", "Song ID is missing", ErrorType.Validation));
            }
            if (trackNr < 1)
            {
                return Result.Failure<AlbumSong>(new Error("TrackNr.Invalid", "Track number is invalid", ErrorType.Validation));
            }
            if (disc < 1)
            {
                return Result.Failure<AlbumSong>(new Error("Disc.Invalid", "Disc number is invalid", ErrorType.Validation));
            }
            var albumSong = new AlbumSong(Guid.NewGuid(), albumID, songID, trackNr, playTime, disc, note);
            albumSong.IsActive = true;
            return Result.Success(albumSong);
        }
    }
}
