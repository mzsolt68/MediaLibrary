using SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Audio
{
    public class AudioFormat : Entity
    {
        public int AudioFormatID { get; set; }
        [Required]
        [Display(Name = "Formátum neve")]
        public string AudioFormatName { get; set; }
    }
}
