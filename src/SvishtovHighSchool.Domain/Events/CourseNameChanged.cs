using System;

namespace SvishtovHighSchool.Domain.Events
{
    public class CourseNameChanged : DomainEvent
    {
        public CourseNameChanged(Guid aggregateId, string name) : base(aggregateId)
        {
            Name = name;
        }

        public string Name { get;}
    }
}