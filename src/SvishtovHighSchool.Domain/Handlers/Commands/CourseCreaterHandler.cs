using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;

namespace SvishtovHighSchool.Domain.Handlers.Commands
{
    public class CourseCreaterHandler : IHandles<CreateCourse>
    {
        private readonly IRepository<Course> _repository;

        public CourseCreaterHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateCourse message)
        {
            var item = new Course(message.Id, message.Name);

            // TODO think a better way to handle version of events
            _repository.Save(item, -1);
        }
    }
}