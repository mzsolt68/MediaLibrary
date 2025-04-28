using MediatR;
using SharedKernel;

namespace Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents a query that returns a result of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the query.</typeparam>
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
