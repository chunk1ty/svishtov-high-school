using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace SvishtovHighSchool.Integration.Receiver
{
    public class AutofacConfiguration
    {
        internal static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Service>().As<IService>();
            builder.RegisterType<ServiceConfiguration>().AsSelf();

            return builder.Build();
        }
    }
}
