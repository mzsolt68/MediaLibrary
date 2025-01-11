using Domain.Models.Audio;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Audio
{
    public class SongEditDto
    {
        public Song Song { get; set; }
        [Display(Name = "Előadók")]
        public List<SongPerformerDto> Performers { get; set; }
        public int SelectedPerformerID { get; set; }
        public IEnumerable<SongPerformer> PerformerList { get; set; }

        public SongEditDto()
        {
            Performers = new List<SongPerformerDto>();
        }
    }
}
