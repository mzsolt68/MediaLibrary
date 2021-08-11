using System.ComponentModel.DataAnnotations;

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
