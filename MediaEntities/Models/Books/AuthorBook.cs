using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    public class AuthorBook
    {
        [Required]
        public int AuthorID { get; set; }
        public Author Author { get; set; }
        [Required]
        public int BookID { get; set; }
        public Book Book { get; set; }
    }
}
