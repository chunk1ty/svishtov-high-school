using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Domain.CourseModule.Commands;

namespace SvishtovHighSchool.Application.Handlers.Course
{
    public class CourseCreatorHandler : INotificationHandler<CreateCourseCommand>
    {
        private readonly IDomainRepository<Domain.CourseModule.Course> _domainRepository;

        public CourseCreatorHandler(IDomainRepository<Domain.CourseModule.Course> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public async Task Handle(CreateCourseCommand notification, CancellationToken cancellationToken)
        {
            var course = new Domain.CourseModule.Course(notification.Name);

            // TODO think a better way to handle version of events
            course.Version = -1;
            await _domainRepository.SaveAsync(course, -1);
        }
    }
}