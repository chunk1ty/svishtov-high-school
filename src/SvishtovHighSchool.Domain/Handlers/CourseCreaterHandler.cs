using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;

namespace SvishtovHighSchool.Domain.Handlers
{
    public class CourseCreaterHandler : IHandles<CourseCreateCommand>
    {
        private readonly IRepository<Course> _repository;

        public CourseCreaterHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public void Handle(CourseCreateCommand message)
        {
            var item = new Course(message.Id, message.Name);

            _repository.Save(item, -1);
        }
    }
}