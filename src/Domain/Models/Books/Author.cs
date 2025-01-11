using SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    public class Author : Entity
    {
        public int AuthorID { get; set; }
        [Required]
        [Display(Name = "Vezetéknév")]
        public string AuthorLastName { get; set; }
        [Display(Name = "Keresztnév")]
        public string AuthorFirstName { get; set; }
        [Display(Name = "Középső név")]
        public string AuthorMiddleName { get; set; }

        public virtual ICollection<AuthorBook> Books { get; set; }
    }
}
