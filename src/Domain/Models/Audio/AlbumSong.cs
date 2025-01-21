using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.Audio
{
    public class AlbumSong : Entity
    {
        private AlbumSong(Guid id, Guid albumID, Guid songID, int trackNr, string playTime, Byte disc, string note) : base(id)
        {
            AlbumID = albumID;
            SongID = songID;
            TrackNr = trackNr;
            PlayTime = playTime;
            Disc = disc;
            Note = note;
        }

        [Required]
        public Guid AlbumID { get; private set; }
        [JsonIgnore]
        public Album Album { get; private set; }
        [Required]
        public Guid SongID { get; private set; }
        public Song Song { get; private set; }
        [Required]
        public int TrackNr { get; private set; }
        [Required]
        [Display(Name = "Játékidő")]
        public string PlayTime { get; private set; }
        [Display(Name = "Lemez")]
        public Byte Disc { get; private set; }
        [Display(Name = "Megjegyzés")]
        public string Note { get; private set; }

        public static Result<AlbumSong> Create(Guid albumID, Guid songID, int trackNr, string playTime, Byte disc, string note)
        {
            if(string.IsNullOrWhiteSpace(playTime))
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
            if(trackNr < 1)
            {
                return Result.Failure<AlbumSong>(new Error("TrackNr.Invalid", "Track number is invalid", ErrorType.Validation));
            }
            if(disc < 1)
            {
                return Result.Failure<AlbumSong>(new Error("Disc.Invalid", "Disc number is invalid", ErrorType.Validation));
            }
            var albumSong = new AlbumSong(Guid.NewGuid(), albumID, songID, trackNr, playTime, disc, note);
            albumSong.IsActive = true;
            return Result.Success(albumSong);
        }
    }
}
