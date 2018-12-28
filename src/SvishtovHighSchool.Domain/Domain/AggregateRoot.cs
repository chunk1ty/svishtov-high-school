using System;
using System.Collections.Generic;
using SvishtovHighSchool.Domain.Events;

namespace SvishtovHighSchool.Domain.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();

        public abstract Guid Id { get; }

        public int Version { get; internal set; }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var @event in history)
            {
                ApplyChange(@event, false);
            }
        }

        protected void ApplyChange(DomainEvent @event)
        {
            ApplyChange(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChange(DomainEvent ev, bool isNew)
        {
            dynamic domainObject = this;
            dynamic @event = ev;
            domainObject.Apply(@event);

            if (isNew)
            {
                _changes.Add(@event);
            }
        }
    }
}