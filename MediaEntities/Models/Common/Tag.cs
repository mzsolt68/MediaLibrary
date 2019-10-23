using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaEntities.Models.Common
{
    public class Tag
    {
        public int TagID { get; set; }
        [Required]
        [Display(Name = "Cimke")]
        public string TagName { get; set; }
    }
}
