namespace MediaLibrary.Common.Dto.Books
{
    public class BookAuthorDTO
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{LastName}, {FirstName} {MiddleName}";
    }
}
