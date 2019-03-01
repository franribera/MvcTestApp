using System.Collections.Generic;

namespace MvcTestApp.Domain.Infrastructure
{
    public abstract class Aggregate : Entity
    {
        // Normally the aggregate class should contain all the stuff related to the domain events
        // But it not applies to this example.
        // It will keep it just to show that Aggregate is not a empty class in a real case.

        //private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        
        //public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        //protected virtual void AddDomainEvent(IDomainEvent newEvent)
        //{
        //    _domainEvents.Add(newEvent);
        //}

        //public virtual void ClearEvents()
        //{
        //    _domainEvents.Clear();
        //}
    }
}