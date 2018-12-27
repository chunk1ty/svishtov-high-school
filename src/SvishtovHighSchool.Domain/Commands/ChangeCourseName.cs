using System;

namespace SvishtovHighSchool.Domain.Commands
{
    public class ChangeCourseName : Command
    {
        public ChangeCourseName(Guid id, string name, int originalVersion)
        {
            Id = id;
            Name = name;
            OriginalVersion = originalVersion;
        }

        public Guid Id { get; }

        public string Name { get; }

        public int OriginalVersion { get; }
    }
}