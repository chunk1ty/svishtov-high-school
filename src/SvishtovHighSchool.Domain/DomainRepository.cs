﻿using System;
using System.Threading.Tasks;
using MediatR;
using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.Domain
{
    public class DomainRepository<T> : IDomainRepository<T>  where T : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IMediator _mediator;

        public DomainRepository(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
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

                await _mediator.Publish(@event);
            }

            aggregate.MarkChangesAsCommitted();
        }
    }
}
