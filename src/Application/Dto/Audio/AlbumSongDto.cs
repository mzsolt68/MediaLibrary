using Domain.Models.Audio;

namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a song in an album.
    /// </summary>
    public class AlbumSongDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the album.
        /// </summary>
        public int AlbumID { get; set; }

        /// <summary>
        /// Gets or sets the title of the song.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the audio format of the song.
        /// </summary>
        public AudioFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the track number of the song in the album.
        /// </summary>
        public string TrackNr { get; set; }

        /// <summary>
        /// Gets or sets the playtime duration of the song.
        /// </summary>
        public string PlayTime { get; set; }
    }
}
