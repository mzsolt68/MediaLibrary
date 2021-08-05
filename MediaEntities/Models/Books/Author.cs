using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Entities.Models.Books
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

        public virtual ICollection<AuthorBook> Books { get; set; }
    }
}
