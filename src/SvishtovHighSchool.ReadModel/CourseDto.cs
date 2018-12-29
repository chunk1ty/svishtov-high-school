using System;
using System.Collections.Generic;
using System.Text;

namespace SvishtovHighSchool.ReadModel
{
    public class CourseDto : IReadEntity
    {
        // TODO should be immutable
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
