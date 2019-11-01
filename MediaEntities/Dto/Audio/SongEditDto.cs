using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Dto.Audio
{
    public class SongEditDto
    {
        public Song Song { get; set; }
        [Display(Name = "Előadók")]
        public List<SongPerformerDto> Performers { get; set; }
        public int SelectedPerformerID { get; set; }
        public IEnumerable<Performer> PerformerList { get; set; }

        public SongEditDto()
        {
            Performers = new List<SongPerformerDto>();
        }
    }
}
