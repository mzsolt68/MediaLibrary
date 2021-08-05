using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Entities.Models.Books
{
    public class FormatBook
    {
        [Required]
        public int FormatID { get; set; }
        public BookFormat Format { get; set; }
        [Required]
        public int BookID { get; set; }
        public Book Book { get; set; }
    }
}
