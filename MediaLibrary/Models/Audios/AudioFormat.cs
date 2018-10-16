using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audios
{
    public class AudioFormat
    {
        public int AudioFormatID { get; set; }
        [Required]
        public string AudioFormatName { get; set; }
    }
}
