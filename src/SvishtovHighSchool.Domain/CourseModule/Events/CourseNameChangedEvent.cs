using System;
using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.Domain.CourseModule.Events
{
    public class CourseNameChangedEvent : DomainEvent
    {
        public CourseNameChangedEvent(Guid aggregateId, string newName) : base(aggregateId)
        {
            NewName = newName;
        }

        public string NewName { get;}
    }
}