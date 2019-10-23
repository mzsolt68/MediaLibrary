using MediaEntities.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaEntities.Models.Books
{
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        [Display(Name = "Könyv címe")]
        public string BookTitle { get; set; }
        [Display(Name = "Kiadás")]
        public string Edition { get; set; }
        [Display(Name = "Kiadó")]
        public Publisher Publisher { get; set; }
        [Display(Name = "Kiadás éve")]
        public string PublishYear { get; set; }
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }
        [Display(Name = "Nyelv")]
        public Language Language { get; set; }

        public ICollection<Author> Authors { get; set; }
        public ICollection<BookFormat> Formats { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
