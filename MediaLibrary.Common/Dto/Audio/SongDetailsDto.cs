using MediaLibrary.Entities.Models.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Common.Dto.Audio
{
    public class SongDetailsDto
    {
        public SongDto Song { get; set; }
        public string Liryc { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }

        [Display(Name = "Album(ok)")]
        public ICollection<AlbumDto> Albums { get; set; }
    }
}
