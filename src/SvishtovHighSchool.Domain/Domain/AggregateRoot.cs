using System;
using System.Collections.Generic;
using SvishtovHighSchool.Domain.Events;

namespace SvishtovHighSchool.Domain.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public abstract Guid Id { get; }

        public int Version { get; internal set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var @event in history)
            {
                ApplyChange(@event, false);
            }
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChange(Event ev, bool isNew)
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