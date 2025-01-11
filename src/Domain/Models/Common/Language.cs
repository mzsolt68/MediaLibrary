using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common
{
    public class Language
    {
        public int LanguageID { get; set; }
        [Required]
        [Display(Name = "Nyelv")]
        public string LanguageName { get; set; }
    }
}
