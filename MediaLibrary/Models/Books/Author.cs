using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Books
{
    public class Author
    {
        public int AuthorID { get; set; }
        [Required]
        [Display(Name = "Vezetéknév")]
        public string AuthorLastName { get; set; }
        [Display(Name = "Keresztnév")]
        public string AuthorFirstName { get; set; }
        [Display(Name = "Keresztnév 2")]
        public string AuthorFirstName2 { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
