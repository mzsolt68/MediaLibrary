using MediatR;

namespace SharedKernel
{
    /// <summary>
    /// Represents a domain event that is part of the domain-driven design pattern.
    /// Implements the <see cref="INotification"/> interface from MediatR to support
    /// notification handling.
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}