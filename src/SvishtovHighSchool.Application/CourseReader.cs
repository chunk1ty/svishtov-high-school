using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SvishtovHighSchool.ReadModel;

namespace SvishtovHighSchool.Application
{
    public class CourseReader
    {
        private readonly IReadOnlyRepository<CourseDto> _courseRepository;

        public CourseReader(IReadOnlyRepository<CourseDto> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> FindAllAsync(Expression<Func<CourseDto, bool>> predicate)
        {
            return await _courseRepository.FindAllAsync(predicate);
        }

        public async Task<CourseDto> GetByIdAsync(string id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }
    }
}
