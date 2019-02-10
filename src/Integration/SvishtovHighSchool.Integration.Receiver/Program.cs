//using Autofac;

//using Topshelf;
//using Topshelf.Autofac;

//namespace SvishtovHighSchool.Integration.Receiver
//{
//    public class Program
//    {
//        public static void Main()
//        {
//            var container = AutofacConfiguration.Bootstrap();

//            HostFactory.Run(hostConfig =>
//            {
//                hostConfig.UseAutofacContainer(container);

//                hostConfig.Service<IService>(configurator =>
//                {
//                    configurator.ConstructUsingAutofacContainer();
//                    configurator.WhenStarted(service => service.Start());
//                    configurator.WhenStopped(service => service.Stop());
//                });

//                hostConfig.RunAsLocalSystem();

//                using (var scope = container.BeginLifetimeScope())
//                {
//                    var configuration = scope.Resolve<ServiceConfiguration>();
//                    hostConfig.SetDescription(configuration.Description);
//                    hostConfig.SetDisplayName(configuration.DisplayName);
//                    hostConfig.SetServiceName(configuration.ServiceName);
//                }
//            });

//            container.Dispose();
//        }
//    }
//}
