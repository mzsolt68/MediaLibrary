using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Books
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }

        public virtual ICollection<Book> PublishedBooks { get; set; }
    }
}
