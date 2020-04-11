using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Dto.Audio
{
    public class PerformerDetailsDto
    {
        public PerformerDto Performer { get; set; }
        public ICollection<SongDto> Songs { get; set; }
    }
}
