using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.ReadModel;

namespace SvishtovHighSchool.Application.Handlers.Events
{
    public class CourseNameChangedHandler : IHandles<CourseNameChangedEvent>
    {
        private readonly IRepository<CourseDto> _courseRepository;

        public CourseNameChangedHandler(IRepository<CourseDto> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Handle(CourseNameChangedEvent @event)
        {
            var course = _courseRepository.GetByIdAsync(@event.AggregateId.ToString()).GetAwaiter().GetResult();

            course.Name = @event.NewName;

            _courseRepository.UpdateAsync(course).GetAwaiter().GetResult();
        }
    }
}
