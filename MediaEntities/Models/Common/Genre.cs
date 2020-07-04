using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Models.Common
{
    public class Genre
    {
        public int GenreID { get; set; }
        [Required]
        [Display(Name = "Műfaj")]
        public string GenreName { get; set; }
        [Required]
        public string GenreType { get; set; }
    }
}
