using System.ComponentModel.DataAnnotations;

namespace SharedKernel
{
    /// <summary>
    /// Represents the base class for all entities in the domain.
    /// Provides common properties and methods for domain entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// A collection of domain events associated with the entity.
        /// </summary>
        private readonly List<IDomainEvent> _domainEvents = [];

        /// <summary>
        /// Gets the list of domain events associated with the entity.
        /// </summary>
        public List<IDomainEvent> DomainEvents => _domainEvents;

        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        [Key]
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Gets the date and time when the entity was last updated.
        /// </summary>
        [Required]
        public DateTime UpdatedAt { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the entity is active.
        /// </summary>
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the entity.</param>
        protected Entity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Raises a new domain event and associates it with the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to raise.</param>
        public void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Sets the active state of the entity and updates the <see cref="UpdatedAt"/> timestamp.
        /// </summary>
        /// <param name="newState">The new active state of the entity.</param>
        public void SetActiveState(bool newState)
        {
            IsActive = newState;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
