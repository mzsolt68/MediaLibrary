namespace Application.Dto.Books
{
    /// <summary>
    /// Represents the data transfer object used to create a new author.
    /// </summary>
    /// <remarks>This class is typically used to encapsulate the necessary information for creating an author
    /// in the system, including their first, middle, and last names.</remarks>
    public class CreateAuthorDTO
    {
        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of the author.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        public string LastName { get; set; }

    }
}
