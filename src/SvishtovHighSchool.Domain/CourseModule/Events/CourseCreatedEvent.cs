using System;
using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.Domain.CourseModule.Events
{
    public class CourseCreatedEvent : DomainEvent
    {
        public CourseCreatedEvent(Guid aggregateId, string name) : base (aggregateId)
        {
            Name = name;
        }

        public string Name { get;}
    }
}