using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Audio
{
    public class SongDetailsViewModel
    {
        public Song Song { get; set; }
        [Display(Name = "Album(ok)")]
        public ICollection<Album> AlbumsOfSong { get; set; }
        [Display(Name = "Előadó(k)")]
        public ICollection<Performer> PerformersOfSong { get; set; }
    }
}
