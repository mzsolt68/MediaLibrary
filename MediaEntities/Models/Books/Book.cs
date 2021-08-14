using MediaLibrary.Entities.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Entities.Models.Books
{
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        [Display(Name = "Könyv címe")]
        public string BookTitle { get; set; }
        [Display(Name = "Kiadás")]
        public string Edition { get; set; }
        public int PublisherID { get; set; }
        [Display(Name = "Kiadó")]
        public Publisher Publisher { get; set; }
        [Display(Name = "Kiadás éve")]
        public string PublishYear { get; set; }
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }
        public int LanguageID { get; set; }
        [Display(Name = "Nyelv")]
        public Language Language { get; set; }

        public virtual ICollection<AuthorBook> Authors { get; set; }
        public virtual ICollection<FormatBook> Formats { get; set; }
        public virtual ICollection<TagBook> Tags { get; set; }
    }
}
