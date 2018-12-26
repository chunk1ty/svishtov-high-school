using System;

namespace SvishtovHighSchool.Domain.Commands
{
    public class CourseCreateCommand : Command
    {
        public CourseCreateCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}