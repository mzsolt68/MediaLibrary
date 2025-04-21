namespace Application.Dto.Books
{
    public class BookDTO
    {
        public Guid BookID { get; set; }
        public string BookTitle { get; set; }
        public ICollection<BookAuthorDTO> Authors { get; set; }
    }
}
