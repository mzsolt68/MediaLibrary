using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Audio
{
    public class PerformerDetailsViewModel
    {
        public Performer Performer { get; set; }
        public ICollection<PerformerSong> Songs { get; set; }
    }
}
