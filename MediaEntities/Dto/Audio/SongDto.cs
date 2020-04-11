using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Entities.Dto.Audio
{
    public class SongDto
    {
        public int SongID { get; set; }
        public string Title { get; set; }

        public ICollection<PerformerDto> Performers { get; set; }
    }
}
