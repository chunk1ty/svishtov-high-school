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

        public Course(Guid id, string name)
        {
            ApplyChange(new CourseCreated(id, name));
        }

        public override Guid Id => _id;

        public string Name => _name;

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("newName");
            }

            ApplyChange(new CourseNameChanged(_id, name));
        }

        public void Apply(CourseCreated courseCreated)
        {
            _id = courseCreated.AggregateId;
            _name = courseCreated.Name;
        }

        public void Apply(CourseNameChanged courseCreated)
        {
            _id = courseCreated.AggregateId;
            _name = courseCreated.Name;
        }
    }
}
