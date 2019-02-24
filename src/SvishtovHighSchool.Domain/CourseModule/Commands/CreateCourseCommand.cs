using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.Domain.CourseModule.Commands
{
    public class CreateCourseCommand : DomainCommand
    {
        public CreateCourseCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}