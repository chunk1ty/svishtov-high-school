using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;

namespace SvishtovHighSchool.Domain.Handlers.Commands
{
    public class CourseNameChangerHandler : IHandles<ChangeCourseName>
    {
        private readonly IRepository<Course> _repository;

        public CourseNameChangerHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public void Handle(ChangeCourseName changeCourseName)
        {
            var course = _repository.GetById(changeCourseName.Id);

            course.ChangeName(changeCourseName.Name);

            _repository.Save(course, changeCourseName.OriginalVersion);
        }
    }
}