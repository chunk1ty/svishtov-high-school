using System.Collections.Generic;

namespace SvishtovHighSchool.ReadModel
{
    public interface IReadModelFacade
    {
        IEnumerable<CourseDto> GetInventoryItems();
    }
}