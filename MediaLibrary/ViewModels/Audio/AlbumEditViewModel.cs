using MediaLibrary.Models.Audio;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Audio
{
    public class AlbumEditViewModel
    {
        public Album Album { get; set; }
        public int AudioFormatID { get; set; }
        public IEnumerable<SelectListItem> AudioFormats { get; set; }
        public List<AlbumSongViewModel> Songs { get; set; }

        public AlbumEditViewModel()
        {
            Songs = new List<AlbumSongViewModel>();
        }
    }
}
