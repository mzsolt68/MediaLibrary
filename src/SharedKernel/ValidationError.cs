namespace SharedKernel
{
    /// <summary>
    /// Represents a validation error that contains one or more individual errors.
    /// </summary>
    public sealed record ValidationError : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class with the specified errors.
        /// </summary>
        /// <param name="errors">An array of <see cref="Error"/> representing the validation errors.</param>
        public ValidationError(Error[] errors)
            : base(
                 "Validation:General",
                 "One or more validation errors occurred.",
                 ErrorType.Validation)
        {
            Errors = errors;
        }

        /// <summary>
        /// Gets the collection of validation errors.
        /// </summary>
        public Error[] Errors { get; }

        /// <summary>
        /// Creates a <see cref="ValidationError"/> instance from a collection of <see cref="Result"/> objects.
        /// </summary>
        /// <param name="results">The collection of <see cref="Result"/> objects to extract errors from.</param>
        /// <returns>A <see cref="ValidationError"/> containing the errors from the failed results.</returns>
        public static ValidationError FromResults(IEnumerable<Result> results) =>
            new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
    }
}
