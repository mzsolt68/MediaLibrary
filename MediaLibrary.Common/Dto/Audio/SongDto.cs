using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Common.Dto.Audio
{
    public class SongDto
    {
        public int SongID { get; set; }
        public string Title { get; set; }

        public ICollection<SongPerformerDto> Performers { get; set; }
    }
}
