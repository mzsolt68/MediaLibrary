using Domain.Models.Audio;

namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for an album.
    /// </summary>
    public class AlbumDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the album.
        /// </summary>
        public int AlbumID { get; set; }

        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the audio format of the album.
        /// </summary>
        public AudioFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the number of discs in the album.
        /// </summary>
        public int Nr_of_discs { get; set; }

        /// <summary>
        /// Gets or sets the number of tracks in the album.
        /// </summary>
        public int Nr_of_tracks { get; set; }
    }
}
