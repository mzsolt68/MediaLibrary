using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Entities.Dto.Audio
{
    public class AudioDiscDto
    {
        public int DiscNumber { get; set; }
        public ICollection<AudioTrackDto> Tracks { get; set; }
    }
}
