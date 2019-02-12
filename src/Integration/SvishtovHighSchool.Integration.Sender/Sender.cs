using System.Configuration;
using System.Threading.Tasks;

using Google.Protobuf;
using Microsoft.Azure.ServiceBus;

namespace SvishtovHighSchool.Integration.Sender
{
    public interface ISender
    {
        Task SendMessagesAsync(ISvishtovHighSchoolMessage message);
    }

    public interface ISvishtovHighSchoolMessage : IMessage
    {
    }

    public class Sender : ISender
    {
        static IQueueClient _queueClient;

        const string ServiceBusConnectionString = "Endpoint=sb://ankk-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AwJS2thFeAk8aqAq6FRnaERpMTc8snjH85PCJSUPUdk=";
        const string QueueName = "ankk-queue";

        public Sender()
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
        }

        //public string ServiceBusConnectionString => ConfigurationManager.AppSettings["serviceBusConnectionString"];
        //public string QueueName => ConfigurationManager.AppSettings["queueName"];

        public async Task SendMessagesAsync(ISvishtovHighSchoolMessage message)
        {
            var me = new Message(Serialize(message));

            await _queueClient.SendAsync(me);

            //Log<Sender>.Info($"Message {message} has been sent.");
        }

        public static byte[] Serialize(ISvishtovHighSchoolMessage course)
        {
            return course.ToByteArray();
        }
    }

}