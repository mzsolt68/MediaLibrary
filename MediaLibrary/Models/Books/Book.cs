using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Books
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public ICollection<Author> Authors { get; set; }
        public string Edition { get; set; }
        public Publisher Publisher { get; set; }
        public string PublishYear { get; set; }
        public string ISBN { get; set; }
        /*Ide kell még egy property a formátumnak
         * el kell dönteni, hogy gyűjtemény legyen
         * és egy könyv példányhoz csatoljuk valamennyit,
         * vagy minden meglévő formátumhoz létrehozunk egy
         * könyv példányt */
    }
}
