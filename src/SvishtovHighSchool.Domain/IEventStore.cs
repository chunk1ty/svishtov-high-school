using System;
using System.Collections.Generic;
using SvishtovHighSchool.Domain.Events;

namespace SvishtovHighSchool.Domain
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}