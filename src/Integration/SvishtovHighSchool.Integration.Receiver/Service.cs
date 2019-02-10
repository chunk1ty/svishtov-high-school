using System;

namespace SvishtovHighSchool.Integration.Receiver
{
    public interface IService
    {
        void Start();

        void Stop();
    }

    public class Service : IService
    {
        private readonly Receiver _receiver;

        public Service(Receiver receiver)
        {
            this._receiver = receiver;
        }

        public void Start()
        {
            Log<Service>.Info("start service");

            _receiver.MainAsync().GetAwaiter().GetResult();
        }

        public void Stop()
        {
            Console.WriteLine("stop service");
        }
    }
}
