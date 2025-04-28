using Domain.Models.Audio;

namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for editing an album.
    /// </summary>
    public class AlbumEditDto
    {
        /// <summary>
        /// Gets or sets the album being edited.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the selected audio format.
        /// </summary>
        public int AudioFormatID { get; set; }

        /// <summary>
        /// Gets or sets the collection of available audio formats.
        /// </summary>
        public IEnumerable<AudioFormat> AudioFormats { get; set; }

        /// <summary>
        /// Gets or sets the list of songs in the album.
        /// </summary>
        public List<AlbumSongDto> Songs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEditDto"/> class.
        /// </summary>
        public AlbumEditDto()
        {
            Songs = new List<AlbumSongDto>();
        }
    }
}
