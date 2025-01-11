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
        public Guid AlbumID { get; set; }
        [JsonIgnore]
        public Album Album { get; set; }
        [Required]
        public Guid SongID { get; set; }
        public Song Song { get; set; }
        [Required]
        public int TrackNr { get; set; }
        [Required]
        [Display(Name = "Játékidő")]
        public string PlayTime { get; set; }
        [Display(Name = "Lemez")]
        public Byte Disc { get; set; }
        [Display(Name = "Megjegyzés")]
        public string Note { get; set; }

        public static AlbumSong Create(Guid albumID, Guid songID, int trackNr, string playTime, Byte disc, string note)
        {
            var albumSong = new AlbumSong(Guid.NewGuid(), albumID, songID, trackNr, playTime, disc, note);
            albumSong.IsActive = true;
            return albumSong;
        }
    }
}
