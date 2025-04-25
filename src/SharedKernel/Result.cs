using System.Diagnostics.CodeAnalysis;

namespace SharedKernel
{
    /// <summary>
    /// Represents the result of an operation, indicating success or failure.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="error">The error associated with the result, if any.</param>
        /// <exception cref="ArgumentException">Thrown when the error state is invalid.</exception>
        public Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the error associated with the result, if any.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Creates a Success result.
        /// </summary>
        /// <returns>A successful <see cref="Result"/>.</returns>
        public static Result Success() => new(true, Error.None);

        /// <summary>
        /// Creates a Failure result with the specified error.
        /// </summary>
        /// <param name="error">The error associated with the failure.</param>
        /// <returns>A failed <see cref="Result"/>.</returns>
        public static Result Failure(Error error) => new(false, error);

        /// <summary>
        /// Creates a Success result with a value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value associated with the success.</param>
        /// <returns>A successful <see cref="Result{TValue}"/>.</returns>
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        /// <summary>
        /// Creates a Failure result with the specified error.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="error">The error associated with the failure.</param>
        /// <returns>A failed <see cref="Result{TValue}"/>.</returns>
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }

    /// <summary>
    /// Represents the result of an operation with an associated value, indicating success or failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value associated with the result.</typeparam>
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value associated with the result.</param>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="error">The error associated with the result, if any.</param>
        public Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value associated with the result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when accessing the value of a failed result.</exception>
        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        /// <summary>
        /// Implicitly converts a value to a Success <see cref="Result{TValue}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Result<TValue>(TValue? value) =>
            value != null
                ? Success(value)
                : Failure<TValue>(Error.NullValue);

        /// <summary>
        /// Creates a validation failure result with the specified error.
        /// </summary>
        /// <param name="error">The error associated with the validation failure.</param>
        /// <returns>A validation failure <see cref="Result{TValue}"/>.</returns>
        public static Result<TValue> ValidationFailure(Error error) => new(default, false, error);
    }
}