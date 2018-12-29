using System;
using System.Linq;
using System.Threading.Tasks;
using SvishtovHighSchool.Domain.Domain;

namespace SvishtovHighSchool.Domain
{
    // TODO fixed it to fit in newest DomainRepository
    public class DomainRepositoryOld<T> : IDomainRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _storage;

        public DomainRepositoryOld(IEventStore storage)
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

        public Task SaveAsync(AggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(AggregateRoot aggregate, int version)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}