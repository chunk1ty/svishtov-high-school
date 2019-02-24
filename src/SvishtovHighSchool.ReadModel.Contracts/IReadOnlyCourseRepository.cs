using System.Collections.Generic;
using System.Threading.Tasks;

namespace SvishtovHighSchool.ReadModel.Contracts
{
    public interface IReadOnlyCourseRepository
    {
        Task<CourseDto> GetByIdAsync(string id);

        Task<IEnumerable<CourseDto>> GetAllAsync();
    }
}