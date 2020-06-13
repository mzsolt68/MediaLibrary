using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Common.Dto.Audio
{
    public class AudioDiscDto
    {
        public int DiscNumber { get; set; }
        public ICollection<AudioTrackDto> Tracks { get; set; }
    }
}
