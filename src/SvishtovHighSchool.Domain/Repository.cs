using System;
using System.Linq;
using SvishtovHighSchool.Domain.Domain;

namespace SvishtovHighSchool.Domain
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges().First(), expectedVersion).GetAwaiter().GetResult();
        }

        public T GetById(Guid id)
        {
            var events = _storage.GetEventsByAggregateId(id).GetAwaiter().GetResult();

            var domainObject = new T();//lots of ways to do this
            
            domainObject.LoadsFromHistory(events);
            return domainObject;
        }
    }
}