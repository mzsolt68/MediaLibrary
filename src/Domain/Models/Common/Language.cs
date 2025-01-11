using SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common
{
    public class Language : Entity
    {
        public int LanguageID { get; set; }
        [Required]
        [Display(Name = "Nyelv")]
        public string LanguageName { get; set; }
    }
}
