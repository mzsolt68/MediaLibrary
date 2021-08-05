using System.ComponentModel.DataAnnotations;
using MediaLibrary.Entities.Models.Common;

namespace MediaLibrary.Entities.Models.Books
{
    public class BookTag
    {
        [Required]
        public int BookID { get; set; }
        public Book Book { get; set; }
        [Required]
        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
