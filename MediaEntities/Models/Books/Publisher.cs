using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Models.Books
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        [Required]
        [Display(Name = "Kiadó")]
        public string PublisherName { get; set; }

        public virtual ICollection<Book> PublishedBooks { get; set; }
    }
}
