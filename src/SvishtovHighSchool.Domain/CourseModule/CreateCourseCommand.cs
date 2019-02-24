using MediatR;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Domain.CourseModule
{
    public class CreateCourseCommand : Command, INotification
    {
        public CreateCourseCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}