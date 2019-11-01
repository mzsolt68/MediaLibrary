using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Dto.Audio
{
    public class AlbumDetailsDto
    {
        public Album Album { get; set; }
        public ICollection<AlbumSong> Details { get; set; }
    }
}
