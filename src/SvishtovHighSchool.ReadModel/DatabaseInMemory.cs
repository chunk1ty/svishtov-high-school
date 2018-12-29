using System.Collections.Generic;
using SvishtovHighSchool.Domain;

namespace SvishtovHighSchool.ReadModel
{
    public static class DatabaseInMemory
    {
        public static List<CourseDto> Courses = new List<CourseDto>();
    }

    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<CourseDto> GetInventoryItems()
        {
            return DatabaseInMemory.Courses;
        }
    }
}