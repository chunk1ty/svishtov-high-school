using System;
using SvishtovHighSchool.Domain.Events;

namespace SvishtovHighSchool.Domain.Domain
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

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("newName");
            }

            ApplyChange(new CourseNameChangedEvent(_id, name));
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
