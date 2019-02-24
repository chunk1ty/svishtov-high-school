using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Domain.CourseModule.Commands;

namespace SvishtovHighSchool.Application.Handlers.Course
{
    public class CourseNameChangerHandler : INotificationHandler<ChangeCourseNameCommand>
    {
        private readonly IDomainRepository<Domain.CourseModule.Course> _domainRepository;

        public CourseNameChangerHandler(IDomainRepository<Domain.CourseModule.Course> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public async Task Handle(ChangeCourseNameCommand notification, CancellationToken cancellationToken)
        {
            var course = await _domainRepository.GetByIdAsync(notification.Id);

            course.ChangeName(notification.Name);

            // TODO think a better way to handle version of events
            await _domainRepository.SaveAsync(course, notification.OriginalVersion);
        }
    }
}