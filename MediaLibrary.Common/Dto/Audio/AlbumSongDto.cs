using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Dto.Audio
{
    public class AlbumSongDto
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public AudioFormat Format { get; set; }
        public string TrackNr { get; set; }
        public string PlayTime { get; set; }
    }
}
