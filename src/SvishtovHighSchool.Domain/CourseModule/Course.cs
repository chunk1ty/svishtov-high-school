using System;
using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.Domain.CourseModule
{
    public class Course : AggregateRoot
    {
        private Guid _id;
        private string _name;

        public Course()
        {
        }

        public Course(string name)
        {
            var id = Guid.NewGuid();
            var courseCreatedEvent = new CourseCreatedEvent(id, name);

            ApplyChange(courseCreatedEvent);
        }

        public override Guid Id => _id;

        public string Name => _name;

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("newName");
            }

            ApplyChange(new CourseNameChangedEvent(_id, newName));
        }

        public void Apply(CourseCreatedEvent courseCreatedEvent)
        {
            _id = courseCreatedEvent.AggregateId;
            _name = courseCreatedEvent.Name;
        }

        public void Apply(CourseNameChangedEvent courseCreated)
        {
            _id = courseCreated.AggregateId;
            _name = courseCreated.NewName;
        }
    }
}
