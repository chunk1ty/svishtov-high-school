using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Ankk.Models;
using Google.Protobuf;
using Microsoft.Azure.ServiceBus;
using SvishtovHighSchool.Application;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Integration.Sender
{
    public class Sender : ISender
    {
        private readonly IQueueClient _queueClient;

        const string ServiceBusConnectionString = "Endpoint=sb://ankk-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AwJS2thFeAk8aqAq6FRnaERpMTc8snjH85PCJSUPUdk=";
        const string QueueName = "ankk-queue";

        public Sender()
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
        }

        public async Task SendMessagesAsync(IDomainMessage domainMessage)
        {
            IMessage protoMessage = null;
            if (typeof(CourseCreatedEvent) == (Type) domainMessage)
            {
                protoMessage = Mapper.ToProto((CourseCreatedEvent) domainMessage);
            }

            if (typeof(CourseNameChangedEvent) == (Type)domainMessage)
            {
                protoMessage = Mapper.ToProto((CourseNameChangedEvent)domainMessage);
            }

            var me = new Message(Serialize(protoMessage));

            await _queueClient.SendAsync(me);
        }

        private static byte[] Serialize(IMessage course)
        {
            return course.ToByteArray();
        }
    }

    internal static class Mapper
    {
        internal static CourseUpdated ToProto(CourseCreatedEvent @event)
        {
            return new CourseUpdated
            {
                Name = @event.Name
            };
        }

        internal static CourseUpdated ToProto(CourseNameChangedEvent @event)
        {
            return new CourseUpdated
            {
                Id = @event.AggregateId.ToString(),
                Name = @event.NewName
            };
        }
    }

    interface IMessageConverter
    {
         IMessage Convert(dynamic @event);
    }

    class CourseCreateConverter : IMessageConverter
    {
        public IMessage Convert(dynamic @event)
        {
            var s = (CourseCreatedEvent) @event;

            return new CourseUpdated
            {
                Name = s.Name
            };
        }
    }

    class CourseUpdateConverter : IMessageConverter
    {
        public IMessage Convert(dynamic @event)
        {
            var s = (CourseNameChangedEvent) @event;

            return new CourseUpdated
            {
                Id = s.AggregateId.ToString(),
                Name = s.NewName
            };
        }
    }
}