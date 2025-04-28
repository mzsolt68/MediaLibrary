namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a data transfer object containing detailed information about an album.
    /// </summary>
    public class AlbumDetailsDto
    {
        /// <summary>
        /// Gets or sets the album information.
        /// </summary>
        public AlbumDto Album { get; set; }

        /// <summary>
        /// Gets or sets the collection of discs associated with the album.
        /// </summary>
        public ICollection<AudioDiscDto> Discs { get; set; }
    }
}
