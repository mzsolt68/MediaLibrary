using MediaLibrary.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Audio
{
    public class SongDetailsViewModel
    {
        public Song Song { get; set; }
        public ICollection<Album> AlbumsOfSong { get; set; }
        public ICollection<Performer> PerformersOfSong { get; set; }
    }
}
