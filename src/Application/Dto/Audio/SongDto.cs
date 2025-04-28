using Domain.Models.Common;

namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a data transfer object for a song.
    /// </summary>
    public class SongDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the song.
        /// </summary>
        public int SongID { get; set; }

        /// <summary>
        /// Gets or sets the title of the song.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the lyrics of the song.
        /// </summary>
        public string Lyric { get; set; }

        /// <summary>
        /// Gets or sets the genre of the song.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the language of the song.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the collection of performers associated with the song.
        /// </summary>
        public ICollection<SongPerformerDto> Performers { get; set; }
    }
}
