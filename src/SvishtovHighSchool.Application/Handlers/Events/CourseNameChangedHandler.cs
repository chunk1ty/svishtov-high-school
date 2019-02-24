using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.ReadModel;
using SvishtovHighSchool.ReadModel.Contracts;

namespace SvishtovHighSchool.Application.Handlers.Events
{
    public class CourseNameChangedHandler : IHandles<CourseNameChangedEvent>
    {
        private readonly ICourseRepository _courseRepository;

        public CourseNameChangedHandler(ICourseRepository courseRepository)
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
