using System.Collections.Generic;

namespace MediaLibrary.Common.Dto.Books
{
    public class BookPublisherDetailsDTO
    {
        public BookPublisherDTO Publisher { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}
