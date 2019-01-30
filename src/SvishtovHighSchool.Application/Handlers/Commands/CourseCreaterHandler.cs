using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;
using SvishtovHighSchool.Infrastructure;

namespace SvishtovHighSchool.Application.Handlers.Commands
{
    public class CourseCreaterHandler : IHandles<CreateCourse>
    {
        private readonly IDomainRepository<Course> _domainRepository;

        public CourseCreaterHandler(IDomainRepository<Course> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Handle(CreateCourse message)
        {
            var course = new Course(message.Name);

            // TODO think a better way to handle version of events
            course.Version = -1;
            _domainRepository.SaveAsync(course, -1).GetAwaiter().GetResult();
        }
    }
}