using MediaLibrary.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Audio
{
    public class AlbumDetailsViewModel
    {
        public Album Album { get; set; }
        public ICollection<AlbumSong> Details { get; set; }
    }
}
