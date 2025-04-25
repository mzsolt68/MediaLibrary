namespace SharedKernel
{
    /// <summary>
    /// Provides an abstraction for accessing the current UTC date and time.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date and time in Coordinated Universal Time (UTC).
        /// </summary>
        DateTime UtcNow { get; }
    }
}
