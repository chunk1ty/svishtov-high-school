using System.Collections.Generic;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Domain.ReadModels;

namespace SvishtovHighSchool.Domain
{
    public interface IReadModelFacade
    {
        IEnumerable<CourseDto> GetInventoryItems();
    }

    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<CourseDto> GetInventoryItems()
        {
            return Database.Courses;
        }
    }
}
