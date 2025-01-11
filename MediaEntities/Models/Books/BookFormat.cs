using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    public class BookFormat
    {
        public int BookFormatID { get; set; }
        [Required]
        [Display(Name = "Formátum")]
        public string BookFormatName { get; set; }

        public virtual ICollection<FormatBook> BooksInFormat { get; set; }
    }
}
