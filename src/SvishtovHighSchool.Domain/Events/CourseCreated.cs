using System;

namespace SvishtovHighSchool.Domain.Events
{
    public class CourseCreated : Event
    {
        public CourseCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get;}

        public string Name { get;}
    }
}