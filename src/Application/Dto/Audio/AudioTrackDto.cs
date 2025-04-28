namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a data transfer object for an audio track.
    /// </summary>
    public class AudioTrackDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the song.
        /// </summary>
        public int SongID { get; set; }

        /// <summary>
        /// Gets or sets the track number of the song.
        /// </summary>
        public int TrackNr { get; set; }

        /// <summary>
        /// Gets or sets the collection of performers for the track.
        /// </summary>
        public ICollection<string> Performer { get; set; }

        /// <summary>
        /// Gets or sets the title of the track.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the playtime of the track in a string format.
        /// </summary>
        public string PlayTime { get; set; }

        /// <summary>
        /// Gets or sets any additional notes about the track.
        /// </summary>
        public string Note { get; set; }
    }
}
