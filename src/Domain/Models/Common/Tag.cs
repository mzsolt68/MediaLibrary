using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Books;
using SharedKernel;

namespace Domain.Models.Common
{
    public class Tag : Entity
    {
        public int TagID { get; set; }
        [Required]
        [Display(Name = "Cimke")]
        public string TagName { get; set; }

        public virtual ICollection<TagBook> BooksOfTag { get; set; }
    }
}
