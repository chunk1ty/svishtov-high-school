using System;
using System.Collections.Generic;
using System.Text;

namespace SvishtovHighSchool.Integration.Receiver
{
    public interface IService
    {
        void Start();

        void Stop();
    }

    public class Service : IService
    {
        public void Start()
        {
            Console.WriteLine("start service");
        }

        public void Stop()
        {
            Console.WriteLine("stop service");
        }
    }
}
