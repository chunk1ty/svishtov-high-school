using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SvishtovHighSchool.ReadModel.Contracts;
using SvishtovHighSchool.ReadModel.Entities;

namespace SvishtovHighSchool.ReadModel.Repositories
{
    public class MongoDbCourseRepository : ICourseRepository
    {
        private readonly SvishtovHighSchoolDbContext _context;

        public MongoDbCourseRepository(SvishtovHighSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _context.Courses.Find(x => true)
                                                .ToListAsync();

            return courses.Select(x => new CourseDto(x.AggregateId, x.Name));
        }

        public async Task<CourseDto> GetByIdAsync(string id)
        {
            var course = await _context.Courses.Find(x => x.Id == id)
                                               .SingleAsync();
            
            return new CourseDto(course.AggregateId, course.Name);
        }

        public async Task AddAsync(CourseDto course)
        {
            var entity = new CourseEntity
            {
                AggregateId = course.AggregateId,
                Name = course.Name
            };

            await _context.Courses.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(CourseDto course)
        {
            var entity = new CourseEntity
            {
                AggregateId = course.AggregateId,
                Name = course.Name
            };

            await _context.Courses.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
