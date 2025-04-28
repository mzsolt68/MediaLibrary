using MediatR;
using SharedKernel;

namespace Application.Abstractions.Messaging
{
    /// <summary>
    /// Defines a handler for processing commands that do not return a response.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to handle.</typeparam>
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand
    {
    }

    /// <summary>
    /// Defines a handler for processing commands that return a response.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to handle.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
    {
    }
}
