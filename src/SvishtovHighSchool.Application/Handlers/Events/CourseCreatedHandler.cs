using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.ReadModel;

namespace SvishtovHighSchool.Application.Handlers.Events
{
    public class CourseCreatedHandler : IHandles<CourseCreatedEvent>
    {
        private readonly IRepository<CourseDto> _courseRepository;

        public CourseCreatedHandler(IRepository<CourseDto> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Handle(CourseCreatedEvent @event)
        {
            var course = new CourseDto
            {
                Id = @event.AggregateId.ToString(),
                Name = @event.Name
            };

            _courseRepository.InsertAsync(course).GetAwaiter().GetResult();
        }
    }
}
