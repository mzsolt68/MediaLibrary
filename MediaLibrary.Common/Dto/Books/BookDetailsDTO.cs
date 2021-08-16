using MediaLibrary.Common.Dto.Common;
using System.Collections.Generic;

namespace MediaLibrary.Common.Dto.Books
{
    public class BookDetailsDTO
    {
        public BookDTO Book { get; set; }
        public string Edition { get; set; }
        public BookPublisherDTO Publisher { get; set; }
        public string PublisYear { get; set; }
        public string ISBN { get; set; }
        public ICollection<BookFormatDTO> Formats { get; set; }
        public LanguageDTO Language { get; set; }
        public ICollection<TagDTO> Tags { get; set; }
    }
}
