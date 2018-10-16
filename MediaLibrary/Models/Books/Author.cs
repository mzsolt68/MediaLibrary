using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Books
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorFirstName2 { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
