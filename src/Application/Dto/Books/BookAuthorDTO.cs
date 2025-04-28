namespace Application.Dto.Books
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a book author.
    /// </summary>
    public class BookAuthorDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the author.
        /// </summary>
        public Guid AuthorID { get; set; }

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

        /// <summary>
        /// Gets the full name of the author in the format "LastName, FirstName MiddleName".
        /// </summary>
        public string FullName => $"{LastName}, {FirstName} {MiddleName}";
    }
}
