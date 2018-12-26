using System;

namespace SvishtovHighSchool.Domain.ReadModels
{
    public class CourseDto
    {
        public Guid Id { get; }

        public string Name { get; }

        public CourseDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}