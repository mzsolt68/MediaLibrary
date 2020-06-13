using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Dto.Audio
{
    public class AlbumEditDto
    {
        public Album Album { get; set; }
        public int AudioFormatID { get; set; }
        public IEnumerable<AudioFormat> AudioFormats { get; set; }
        public List<AlbumSongDto> Songs { get; set; }

        public AlbumEditDto()
        {
            Songs = new List<AlbumSongDto>();
        }
    }
}
