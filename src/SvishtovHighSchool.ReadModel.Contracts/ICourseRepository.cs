using System.Threading.Tasks;

namespace SvishtovHighSchool.ReadModel.Contracts
{
    public interface ICourseRepository : IReadOnlyCourseRepository
    {
        Task AddAsync(CourseDto course);

        Task UpdateAsync(CourseDto course);
    }
}