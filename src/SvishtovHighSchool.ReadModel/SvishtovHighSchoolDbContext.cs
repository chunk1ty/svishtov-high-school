using MongoDB.Driver;
using SvishtovHighSchool.ReadModel.Entities;

namespace SvishtovHighSchool.ReadModel
{
    public class SvishtovHighSchoolDbContext
    {
        private readonly IMongoDatabase _database;

        public SvishtovHighSchoolDbContext(IMongoDatabase mongoDatabases)
        {
            _database = mongoDatabases;
        }

        public IMongoCollection<CourseEntity> Courses => _database.GetCollection<CourseEntity>("Courses");
    }
}
