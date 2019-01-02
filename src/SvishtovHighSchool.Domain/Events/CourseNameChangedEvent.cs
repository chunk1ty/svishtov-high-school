using System;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Domain.Events
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