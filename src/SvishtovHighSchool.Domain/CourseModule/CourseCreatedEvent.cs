using System;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Domain.CourseModule
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