using System;

namespace SvishtovHighSchool.Domain.ReadModels
{
    public class CourseDto
    {
        public Guid Id { get; }

        public string Name { get; set; }

        public CourseDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}