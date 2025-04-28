using MediatR;
using SharedKernel;

namespace Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents a command that does not return a response.
    /// </summary>
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    /// <summary>
    /// Represents a command that returns a response of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {
    }

    /// <summary>
    /// Represents the base interface for all command types.
    /// </summary>
    public interface IBaseCommand
    {
    }
}
