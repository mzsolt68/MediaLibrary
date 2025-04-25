namespace SharedKernel
{
    ///<summary>
    /// Represents an error with a code, message, and type.
    /// </summary>
    public record Error
    {
        /// <summary>
        /// Represents no error.
        /// </summary>
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        /// <summary>
        /// Represents an error for a null value.
        /// </summary>
        public static readonly Error NullValue = new("General.Null", "Null value was provided", ErrorType.Failure);

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record.
        /// </summary>
        /// <param name="code">The unique code identifying the error.</param>
        /// <param name="message">The message describing the error.</param>
        /// <param name="type">The type of the error.</param>
        public Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        /// <summary>
        /// Gets the unique code identifying the error.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the message describing the error.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        public ErrorType Type { get; }

        /// <summary>
        /// Creates a failure error.
        /// </summary>
        /// <param name="code">The unique code identifying the error.</param>
        /// <param name="message">The message describing the error.</param>
        /// <returns>A new <see cref="Error"/> instance representing a failure.</returns>
        public static Error Failure(string code, string message) => new Error(code, message, ErrorType.Failure);

        /// <summary>
        /// Creates a not found error.
        /// </summary>
        /// <param name="code">The unique code identifying the error.</param>
        /// <param name="message">The message describing the error.</param>
        /// <returns>A new <see cref="Error"/> instance representing a not found error.</returns>
        public static Error NotFound(string code, string message) => new Error(code, message, ErrorType.NotFound);

        /// <summary>
        /// Creates a conflict error.
        /// </summary>
        /// <param name="code">The unique code identifying the error.</param>
        /// <param name="message">The message describing the error.</param>
        /// <returns>A new <see cref="Error"/> instance representing a conflict error.</returns>
        public static Error Conflict(string code, string message) => new Error(code, message, ErrorType.Conflict);

        /// <summary>
        /// Creates a problem error.
        /// </summary>
        /// <param name="code">The unique code identifying the error.</param>
        /// <param name="message">The message describing the error.</param>
        /// <returns>A new <see cref="Error"/> instance representing a problem error.</returns>
        public static Error Problem(string code, string message) => new Error(code, message, ErrorType.Problem);
    }
}
