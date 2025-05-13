namespace SharedKernel
{
    /// <summary>
    /// SearchType enumeration defines various types of search operations.
    /// </summary>
    public enum SearchType
    {
        Exact = 0,
        Contains = 1,
        StartsWith = 2,
        EndsWith = 3,
        GreaterThan = 4,
        GreaterThanOrEqual = 5,
        LessThan = 6,
        LessThanOrEqual = 7,
        NotEqual = 8,
        Casesensitive = 9
    }
}
