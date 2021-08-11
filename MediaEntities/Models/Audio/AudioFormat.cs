using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Entities.Models.Audio
{
    public class AudioFormat
    {
        public int AudioFormatID { get; set; }
        [Required]
        [Display(Name = "Formátum neve")]
        public string AudioFormatName { get; set; }
    }
}
