using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Domain.ReadModels;

namespace SvishtovHighSchool.Domain.Handlers.Events
{
    public class CourseCreatedHandler : IHandles<CourseCreated>
    {
        public void Handle(CourseCreated message)
        {
            Database.Courses.Add(new CourseDto(message.Id, message.Name));
        }
    }
}
