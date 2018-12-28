using System;

namespace SvishtovHighSchool.Domain.Events
{
    public class CourseCreated : DomainEvent
    {
        public CourseCreated(Guid aggregateId, string name) : base (aggregateId)
        {
            Name = name;
        }

        public string Name { get;}
    }
}