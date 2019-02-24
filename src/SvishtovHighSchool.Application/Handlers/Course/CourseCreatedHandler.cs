using System.Threading;
using System.Threading.Tasks;

using MediatR;

using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Domain.CourseModule.Events;
using SvishtovHighSchool.ReadModel.Contracts;

namespace SvishtovHighSchool.Application.Handlers.Course
{
    public class CourseCreatedHandler : INotificationHandler<CourseCreatedEvent>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISender _sender;

        public CourseCreatedHandler(ICourseRepository courseRepository, ISender sender)
        {
            _courseRepository = courseRepository;
            _sender = sender;
        }

        public async Task Handle(CourseCreatedEvent notification, CancellationToken cancellationToken)
        {
            var course = new CourseDto(notification.AggregateId.ToString(), notification.Name);

            await _courseRepository.AddAsync(course);

            _sender.SendMessagesAsync<CourseCreatedEvent>(notification).GetAwaiter().GetResult();
        }
    }
}
