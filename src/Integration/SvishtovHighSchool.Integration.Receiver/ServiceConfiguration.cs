using System.Configuration;

namespace SvishtovHighSchool.Integration.Receiver
{
    public class ServiceConfiguration 
    {
        public string ServiceName => ConfigurationManager.AppSettings["serviceName"];

        public string DisplayName => ConfigurationManager.AppSettings["serviceDisplayName"];

        public string Description => ConfigurationManager.AppSettings["serviceDescription"];

        public string Version => ConfigurationManager.AppSettings["version"];
    }
}