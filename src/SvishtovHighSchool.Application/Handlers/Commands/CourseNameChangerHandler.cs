using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;

namespace SvishtovHighSchool.Application.Handlers.Commands
{
    public class CourseNameChangerHandler : IHandles<ChangeCourseNameCommand>
    {
        private readonly IDomainRepository<Course> _domainRepository;

        public CourseNameChangerHandler(IDomainRepository<Course> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Handle(ChangeCourseNameCommand command)
        {
            var course = _domainRepository.GetByIdAsync(command.Id).GetAwaiter().GetResult();

            course.ChangeName(command.Name);
            
            // TODO think a better way to handle version of events
            _domainRepository.SaveAsync(course, command.OriginalVersion).GetAwaiter().GetResult();
        }
    }
}