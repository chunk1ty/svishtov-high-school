using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using EventStore.ClientAPI;
using Newtonsoft.Json;

using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.EventStore
{
    public class EventStoreEventStore : IEventStore
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreEventStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task<AppendResult> SaveEvents(Guid aggregateId, DomainEvent @event, int expectedVersion)
        {
            var eventData = new EventData(
                @event.AggregateId,
                @event.GetType().AssemblyQualifiedName,
                true,
                Serialize(@event),
                Encoding.UTF8.GetBytes("{}"));

            var writeResult = await _connection.AppendToStreamAsync(
                aggregateId.ToString(),
                expectedVersion,
                eventData);

            return new AppendResult(writeResult.NextExpectedVersion);
        }

        public async Task<List<DomainEvent>> GetEventsByAggregateId(Guid aggregateId)
        {
            var domainEvents = new List<DomainEvent>();
            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;

            do
            {
                currentSlice = await _connection.ReadStreamEventsForwardAsync(
                    aggregateId.ToString(),
                    nextSliceStart,
                    200,
                    false);

                if (currentSlice.Status != SliceReadStatus.Success)
                {
                    throw new EventStoreAggregateNotFoundException($"Aggregate {aggregateId} not found");
                }

                nextSliceStart = currentSlice.NextEventNumber;

                foreach (var resolvedEvent in currentSlice.Events)
                {
                    var @event = Deserialize(resolvedEvent.Event.EventType, resolvedEvent.Event.Data);

                    domainEvents.Add(@event);
                }
            } while (!currentSlice.IsEndOfStream);

            return domainEvents;
        }

        private DomainEvent Deserialize(string eventType, byte[] data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };

            return (DomainEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType(eventType), settings);
        }

        private byte[] Serialize(DomainEvent @event)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
        }
    }
}
