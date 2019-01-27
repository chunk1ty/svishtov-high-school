using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MessageDefinition;
using Microsoft.Azure.ServiceBus;
using ProtoBuf;

namespace Sender
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://ankk-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AwJS2thFeAk8aqAq6FRnaERpMTc8snjH85PCJSUPUdk=";
        const string QueueName = "ankk-queue";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");
            
            await SendMessagesAsync(numberOfMessages);

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    string messageBody = $"Message {i}";
                   
                    var course = new Course
                    {
                        Name = $"Course {i}",
                        SomeInt = i,
                        CreatedOn = DateTime.UtcNow
                    };
                    
                    var message = new Message(Serialize(course));
                    Console.WriteLine($"Sending message: {messageBody}");
                    
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

        public static byte[] Serialize(Course c)
        {
            byte[] msgOut;

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, c);
                msgOut = stream.GetBuffer();
                stream.Position = 0;
            }

            return msgOut;
        }
    }
}
