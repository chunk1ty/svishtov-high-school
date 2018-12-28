using System.Linq;
using SvishtovHighSchool.Domain.Events;

namespace SvishtovHighSchool.Domain.Handlers.Events
{
    public class CourseNameChangedHandler : IHandles<CourseNameChanged>
    {
        public void Handle(CourseNameChanged courseNameChanged)
        {
            var course = Database.Courses.SingleOrDefault(x => x.Id == courseNameChanged.AggregateId);

            course.Name = courseNameChanged.Name;
        }
    }
}
