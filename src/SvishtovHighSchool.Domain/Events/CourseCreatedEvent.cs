using System;

namespace SvishtovHighSchool.Domain.Events
{
    //TODO naming ?
    public class CourseCreatedEvent : DomainEvent
    {
        public CourseCreatedEvent(Guid aggregateId, string name) : base (aggregateId)
        {
            Name = name;
        }

        public string Name { get;}
    }
}