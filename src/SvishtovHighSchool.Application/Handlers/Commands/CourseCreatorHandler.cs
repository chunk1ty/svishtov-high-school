using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.CourseModule;

namespace SvishtovHighSchool.Application.Handlers.Commands
{
    public class CourseCreatorHandler : INotificationHandler<CreateCourseCommand> //  IHandles<CreateCourseCommand>,
    {
        private readonly IDomainRepository<Course> _domainRepository;

        public CourseCreatorHandler(IDomainRepository<Course> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        //public void Handle(CreateCourseCommand message)
        //{
        //    var course = new Course(message.Name);

        //    // TODO think a better way to handle version of events
        //    course.Version = -1;
        //    _domainRepository.SaveAsync(course, -1).GetAwaiter().GetResult();
        //}

        public async Task Handle(CreateCourseCommand notification, CancellationToken cancellationToken)
        {
            var course = new Course(notification.Name);

            // TODO think a better way to handle version of events
            course.Version = -1;
            await _domainRepository.SaveAsync(course, -1);
        }
    }
}