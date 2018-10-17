using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audio
{
    public class AudioFormat
    {
        public int AudioFormatID { get; set; }
        [Required]
        [Display(Name = "Formátum neve")]
        public string AudioFormatName { get; set; }
    }
}
