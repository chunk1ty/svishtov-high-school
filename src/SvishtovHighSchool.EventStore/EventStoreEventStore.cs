using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Infrastructure;

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
            try
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
            catch (EventStoreConnectionException ex)
            {
                throw new EventStoreCommunicationException($"Error while appending event {@event.AggregateId} for aggregate {aggregateId}", ex);
            }
        }

        public async Task<List<DomainEvent>> GetEventsByAggregateId(Guid aggregateId)
        {
            try
            {
                var ret = new List<DomainEvent>();
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
                        var e = Deserialize(resolvedEvent.Event.EventType, resolvedEvent.Event.Data);

                        ret.Add(e);
                    }
                } while (!currentSlice.IsEndOfStream);

                return ret;
            }
            catch (EventStoreConnectionException ex)
            {
                throw new EventStoreCommunicationException($"Error while reading events for aggregate {aggregateId}", ex);
            }
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

    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(
            MemberInfo member,
            MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (!prop.Writable)
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    var hasPrivateSetter = property.GetSetMethod(true) != null;
                    prop.Writable = hasPrivateSetter;
                }
            }

            return prop;
        }
    }

    [Serializable]
    public class EventStoreException : Exception
    {
        public EventStoreException() { }
        public EventStoreException(string message) : base(message) { }
        public EventStoreException(string message, Exception inner) : base(message, inner) { }
        protected EventStoreException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class EventStoreAggregateNotFoundException : EventStoreException
    {
        public EventStoreAggregateNotFoundException() { }
        public EventStoreAggregateNotFoundException(string message) : base(message) { }
        public EventStoreAggregateNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected EventStoreAggregateNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class EventStoreCommunicationException : EventStoreException
    {
        public EventStoreCommunicationException() { }
        public EventStoreCommunicationException(string message) : base(message) { }
        public EventStoreCommunicationException(string message, Exception inner) : base(message, inner) { }
        protected EventStoreCommunicationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
