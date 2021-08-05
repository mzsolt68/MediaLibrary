using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaLibrary.Entities.Models.Books;

namespace MediaLibrary.Entities.Models.Common
{
    public class Tag
    {
        public int TagID { get; set; }
        [Required]
        [Display(Name = "Cimke")]
        public string TagName { get; set; }

        public virtual ICollection<BookTag> BooksOfTag { get; set; }
    }
}
