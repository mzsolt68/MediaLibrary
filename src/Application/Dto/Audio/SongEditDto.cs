using Domain.Models.Audio;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Audio
{
    /// <summary>
    /// Represents a data transfer object for editing a song.
    /// </summary>
    public class SongEditDto
    {
        /// <summary>
        /// Gets or sets the song being edited.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the list of performers associated with the song.
        /// </summary>
        [Display(Name = "Előadók")]
        public List<SongPerformerDto> Performers { get; set; }

        /// <summary>
        /// Gets or sets the ID of the selected performer.
        /// </summary>
        public int SelectedPerformerID { get; set; }

        /// <summary>
        /// Gets or sets the list of available performers for selection.
        /// </summary>
        public IEnumerable<SongPerformer> PerformerList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SongEditDto"/> class.
        /// </summary>
        public SongEditDto()
        {
            Performers = new List<SongPerformerDto>();
        }
    }
}
