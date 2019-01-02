using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Domain
{
    public interface IEventStore
    {
        Task<AppendResult> SaveEvents(Guid aggregateId, DomainEvent @event, int expectedVersion);

        Task<List<DomainEvent>> GetEventsByAggregateId(Guid aggregateId);
    }

    public class AppendResult
    {
        public AppendResult(long nextExpectedVersion)
        {
            NextExpectedVersion = nextExpectedVersion;
        }

        public long NextExpectedVersion { get; }
    }
}