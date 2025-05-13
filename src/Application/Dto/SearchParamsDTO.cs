using SharedKernel;

namespace Application.Dto
{
    /// <summary>
    /// Data Transfer Object for search parameters, including pagination, filtering, and sorting options.
    /// </summary>
    public class SearchParamsDTO
    {
        /// <summary>
        /// Gets or sets the page number for pagination.
        /// The default value is 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination.
        /// The default value is 10.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the collection of search parameters to filter results.
        /// </summary>
        public ICollection<SearchParam> SearchParams { get; set; } = [];

        /// <summary>
        /// Gets or sets the sort order for the results.
        /// </summary>
        public string? SortOrder { get; set; }
    }

    /// <summary>
    /// Represents a single search parameter used for filtering results.
    /// </summary>
    public class SearchParam
    {
        /// <summary>
        /// Gets or sets the name of the property to search.
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value to search for.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the match type for the search.
        /// The default value is <see cref="SearchType.Contains"/>.
        /// </summary>
        public SearchType MatchType { get; set; } = SearchType.Contains;
    }
}
