using Application.Abstractions.Messaging;

namespace Application.Books
{
    public class CreateAuthorCommand : ICommand<Guid>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
    }
}
