using System;

namespace SvishtovHighSchool.Domain.Events
{
    public class CourseNameChanged : Event
    {
        public CourseNameChanged(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get;}

        public string Name { get;}
    }
}