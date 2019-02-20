using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.ReadModel;

namespace SvishtovHighSchool.Application.Handlers.Events
{
    public class CourseCreatedHandler : IHandles<CourseCreatedEvent>
    {
        private readonly IRepository<CourseDto> _courseRepository;
        private readonly ISender _sender;

        public CourseCreatedHandler(IRepository<CourseDto> courseRepository, ISender sender)
        {
            _courseRepository = courseRepository;
            _sender = sender;
        }

        public void Handle(CourseCreatedEvent @event)
        {
            var course = new CourseDto
            {
                Id = @event.AggregateId.ToString(),
                Name = @event.Name
            };

            _courseRepository.InsertAsync(course).GetAwaiter().GetResult();

            _sender.SendMessagesAsync<CourseCreatedEvent>(@event).GetAwaiter().GetResult();
        }
    }
}
