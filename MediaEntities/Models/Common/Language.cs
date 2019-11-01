using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Models.Common
{
    public class Language
    {
        public int LanguageID { get; set; }
        [Required]
        [Display(Name = "Nyelv")]
        public string LanguageName { get; set; }
    }
}
