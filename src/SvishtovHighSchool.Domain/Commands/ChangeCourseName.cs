using System;

namespace SvishtovHighSchool.Domain.Commands
{
    public class ChangeCourseName : Command
    {
        public ChangeCourseName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}