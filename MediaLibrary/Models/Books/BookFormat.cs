using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Books
{
    public class BookFormat
    {
        public int BookFormatID { get; set; }
        [Required]
        [Display(Name = "Formátum")]
        public string BookFormatName { get; set; }
    }
}
