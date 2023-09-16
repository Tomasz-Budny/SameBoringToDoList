namespace SameBoringToDoList.Shared.Domain.Primitives
{
    public abstract class AggregateRoot<T>: Entity<T>
    {
        protected AggregateRoot(T id): base(id)
        {
        }

        private readonly List<IDomainEvent> _events = new();

        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
    }
}
