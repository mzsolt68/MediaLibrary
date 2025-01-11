using SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books
{
    public class Publisher : Entity
    {
        public int PublisherID { get; set; }
        [Required]
        [Display(Name = "Kiadó")]
        public string PublisherName { get; set; }

        public virtual ICollection<Book> PublishedBooks { get; set; }
    }
}
