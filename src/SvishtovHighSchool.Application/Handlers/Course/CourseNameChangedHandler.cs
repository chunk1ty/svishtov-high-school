using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Domain.CourseModule.Events;
using SvishtovHighSchool.ReadModel.Contracts;

namespace SvishtovHighSchool.Application.Handlers.Course
{
    public class CourseNameChangedHandler : INotificationHandler<CourseNameChangedEvent>
    {
        private readonly ICourseRepository _courseRepository;

        public CourseNameChangedHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task Handle(CourseNameChangedEvent notification, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(notification.AggregateId.ToString());

            course.Name = notification.NewName;

            await _courseRepository.UpdateAsync(course);
        }
    }
}
