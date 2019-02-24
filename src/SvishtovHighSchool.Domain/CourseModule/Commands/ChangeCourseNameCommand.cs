using System;
using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.Domain.CourseModule.Commands
{
    public class ChangeCourseNameCommand : DomainCommand
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