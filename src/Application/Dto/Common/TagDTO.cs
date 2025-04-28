namespace Application.Dto.Common
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a tag.
    /// </summary>
    public class TagDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the tag.
        /// </summary>
        public Guid TagID { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string TagName { get; set; }
    }
}
