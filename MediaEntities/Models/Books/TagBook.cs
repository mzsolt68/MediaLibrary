using System.ComponentModel.DataAnnotations;
using Domain.Models.Common;

namespace Domain.Models.Books
{
    public class TagBook
    {
        [Required]
        public int BookID { get; set; }
        public Book Book { get; set; }
        [Required]
        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
