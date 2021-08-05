using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Entities.Models.Books
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
