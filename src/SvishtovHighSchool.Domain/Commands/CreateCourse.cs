using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Domain.Commands
{
    public class CreateCourse : Command
    {
        public CreateCourse(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}