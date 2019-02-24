using Ankk.Models;
using Google.Protobuf;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Domain.CourseModule.Events;

namespace SvishtovHighSchool.Integration.Converters
{
    public class CourseCreateConverter : IMessageConverter
    {
        public IMessage ToPayload(object @event)
        {
            var s = (CourseCreatedEvent) @event;

            return new CourseUpdated
            {
                Name = s.Name
            };
        }
    }
}