using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.Azure.ServiceBus;
using SvishtovHighSchool.Application;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.Integration.Converters;

namespace SvishtovHighSchool.Integration.Sender
{
    public class Sender : ISender
    {
        private readonly IQueueClient _queueClient;

        const string ServiceBusConnectionString = "Endpoint=sb://ankk-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AwJS2thFeAk8aqAq6FRnaERpMTc8snjH85PCJSUPUdk=";
        const string QueueName = "ankk-queue";

        private static readonly Dictionary<Type, IMessageConverter> _converters = new Dictionary<Type, IMessageConverter>();

        public Sender()
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            //TODO extract to seperate config class
            _converters.Add(typeof(CourseCreatedEvent), new CourseCreateConverter());
            _converters.Add(typeof(CourseNameChangedEvent), new CourseUpdateConverter());
        }

        public async Task SendMessagesAsync<T>(T domainMessage) where T : IDomainMessage
        {
            if (_converters.TryGetValue(typeof(T), out IMessageConverter converter))
            {
                var message = converter.ToPayload(domainMessage);

                var me = new Message(Serialize(message));

                await _queueClient.SendAsync(me);
            }
            else
            {
                throw new Exception("Cannot find converter");
            }
        }

        private static byte[] Serialize(IMessage course)
        {
            return course.ToByteArray();
        }
    }
}