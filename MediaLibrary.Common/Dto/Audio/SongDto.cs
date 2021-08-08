using MediaLibrary.Entities.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Common.Dto.Audio
{
    public class SongDto
    {
        public int SongID { get; set; }
        public string Title { get; set; }
        public string Lyric { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }

        public ICollection<SongPerformerDto> Performers { get; set; }
    }
}
