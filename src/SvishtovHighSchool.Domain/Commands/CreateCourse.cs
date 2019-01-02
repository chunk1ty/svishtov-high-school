using System;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Domain.Commands
{
    public class CreateCourse : Command
    {
        public CreateCourse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}