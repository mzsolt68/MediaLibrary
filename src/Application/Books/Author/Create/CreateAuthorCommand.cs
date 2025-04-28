using Application.Abstractions.Messaging;

namespace Application.Books
{
    /// <summary>
    /// Represents a command to create a new author.
    /// </summary>
    public class CreateAuthorCommand : ICommand<Guid>
    {
        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of the author.
        /// </summary>
        public string MiddleName { get; set; }
    }
}
