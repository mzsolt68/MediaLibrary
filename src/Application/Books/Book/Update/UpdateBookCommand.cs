using Application.Abstractions.Messaging;

namespace Application.Books
{
    public sealed record UpdateBookCommand : ICommand
    {
        public Guid BookID { get; set; }
        public string BookTitle { get; set; }
        public string Edition { get; set; }
        public Guid PublisherID { get; set; }
        public string PublishYear { get; set; }
        public string ISBN { get; set; }
        public Guid LanguageID { get; set; }
        public ICollection<Guid> AuthorIDs { get; set; }
        public ICollection<Guid> FormatIDs { get; set; }
        public ICollection<Guid> TagIDs { get; set; }
    }
}