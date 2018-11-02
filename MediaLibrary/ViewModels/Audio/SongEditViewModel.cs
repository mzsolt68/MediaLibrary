using MediaLibrary.Models.Audio;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.ViewModels.Audio
{
    public class SongEditViewModel
    {
        public Song Song { get; set; }
        [Display(Name = "Előadók")]
        public List<SongPerformerViewModel> Performers { get; set; }
        public int SelectedPerformerID { get; set; }
        public IEnumerable<SelectListItem> PerformerList { get; set; }

        public SongEditViewModel()
        {
            Performers = new List<SongPerformerViewModel>();
        }
    }
}
