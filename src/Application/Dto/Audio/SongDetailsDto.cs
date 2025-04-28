using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents detailed information about a song, including its lyrics, genre, language, and associated albums.
    /// </summary>
    public class SongDetailsDto
    {
        /// <summary>
        /// Gets or sets the song information.
        /// </summary>
        public SongDto Song { get; set; }

        /// <summary>
        /// Gets or sets the lyrics of the song.
        /// </summary>
        public string Liryc { get; set; }

        /// <summary>
        /// Gets or sets the genre of the song.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the language of the song.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the collection of albums associated with the song.
        /// </summary>
        [Display(Name = "Album(ok)")]
        public ICollection<AlbumSongDto> Albums { get; set; }
    }
}
