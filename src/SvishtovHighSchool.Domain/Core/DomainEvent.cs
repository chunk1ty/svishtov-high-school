using System;

namespace SvishtovHighSchool.Domain.Core
{
    public class DomainEvent : IDomainMessage
    {
        // TODO Version or CreatedOn ?
        public int Version;

        public Guid AggregateId { get; }

        public Guid EventId { get; }

        public DateTime CreatedOn { get; }

        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            EventId = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
    }
}