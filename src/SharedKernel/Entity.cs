namespace SharedKernel
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public List<IDomainEvent> DomainEvents => _domainEvents;

        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public bool IsActive { get; protected set; }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void SetActiveState(bool newState)
        {
            IsActive = newState;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
