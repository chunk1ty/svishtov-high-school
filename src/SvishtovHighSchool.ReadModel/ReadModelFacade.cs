using System.Collections.Generic;
using System.Threading.Tasks;

namespace SvishtovHighSchool.ReadModel
{
    public interface IReadModelFacade
    {
        Task<IEnumerable<CourseDto>> GetCoursesAsync();

        Task<CourseDto> GetCourseByIdAsync(string id);
    }

    public class ReadModelFacade : IReadModelFacade
    {
        private readonly IReadOnlyRepository<CourseDto> _courseRepository;

        public ReadModelFacade(IReadOnlyRepository<CourseDto> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
        {
            return await _courseRepository.FindAllAsync(x => true);
        }

        public async Task<CourseDto> GetCourseByIdAsync(string id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }
    }
}