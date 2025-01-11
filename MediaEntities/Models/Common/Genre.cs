using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common
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
