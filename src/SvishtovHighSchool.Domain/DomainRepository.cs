using System;
using System.Linq;
using System.Threading.Tasks;
using SvishtovHighSchool.Domain.Domain;

namespace SvishtovHighSchool.Domain
{
    public class DomainRepository<T> : IDomainRepository<T>  where T : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _publisher;

        public DomainRepository(IEventStore eventStore, IEventPublisher publisher)
        {
            _eventStore = eventStore;
            _publisher = publisher;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var events = await _eventStore.GetEventsByAggregateId(id);

            var domainObject = new T();

            domainObject.LoadsFromHistory(events);

            return domainObject;
        }

        public async Task SaveAsync(AggregateRoot aggregate, int version)
        {
            var unCommittedEvents = aggregate.GetUncommittedChanges();

            foreach (var @event in unCommittedEvents)
            {
                await _eventStore.SaveEvents(aggregate.Id, @event, version);

                _publisher.Publish(@event);
            }

            aggregate.MarkChangesAsCommitted();
        }
    }
}
