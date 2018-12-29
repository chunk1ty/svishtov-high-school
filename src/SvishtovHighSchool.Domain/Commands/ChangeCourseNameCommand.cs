using System;

namespace SvishtovHighSchool.Domain.Commands
{
    public class ChangeCourseNameCommand : Command
    {
        public ChangeCourseNameCommand(Guid id, string name, int originalVersion)
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