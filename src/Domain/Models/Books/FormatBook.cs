using SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    public class FormatBook : Entity
    {
        [Required]
        public int FormatID { get; set; }
        public BookFormat Format { get; set; }
        [Required]
        public int BookID { get; set; }
        public Book Book { get; set; }
    }
}
