using System.Collections.Generic;

namespace MediaLibrary.Common.Dto.Books
{
    public class BookAuthorDetailsDTO
    {
        public BookAuthorDTO Author { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}
