using System;

using EventStore.ClientAPI;
using MongoDB.Driver;

using SvishtovHighSchool.Application.Handlers.Commands;
using SvishtovHighSchool.Application.Handlers.Events;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.EventStore;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.Integration.Sender;
using SvishtovHighSchool.ReadModel;

namespace SvishtovHighSchool.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // configure EventStore
            var connection = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            connection.ConnectAsync().Wait();
            var storage = new EventStoreEventStore(connection);

            // configure MongoDB
            var mongoDatabase = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("SvishtovHighSchool");
            var courseRepository = new MongoDbRepository<CourseDto>(mongoDatabase);

            var bus = new FakeBus();

            var rep = new DomainRepository<Course>(storage, bus);

            // create course 
            var courseCreater = new CourseCreaterHandler(rep);
            bus.RegisterHandler<CreateCourse>(courseCreater.Handle);
            //var courseCreatedHandler = new CourseCreatedHandler(courseRepository);
            //bus.RegisterHandler<CourseCreatedEvent>(courseCreatedHandler.Handle);

            //var courseId = Guid.NewGuid();
            //var courseName = "Math";

            //var courseCreateCommand = new CreateCourse(courseName);

            //bus.Send(courseCreateCommand);

            //var course = courseRepository.FindAllAsync(x => true);

            //// change course name
            //var changeCourseName = new CourseNameChangerHandler(rep);
            //bus.RegisterHandler<ChangeCourseNameCommand>(changeCourseName.Handle);
            //var courseNameChangedHandler = new CourseNameChangedHandler(courseRepository);
            //bus.RegisterHandler<CourseNameChangedEvent>(courseNameChangedHandler.Handle);

            //var changeCoursName = new ChangeCourseNameCommand(courseId, "Chemestry", 0);

            //bus.Send(changeCoursName);

            //course = courseRepository.FindAllAsync(x => true);


            //var changeCoursName1 = new ChangeCourseNameCommand(courseId, "Ankk", 1);

            //bus.Send(changeCoursName1);

            //course = courseRepository.FindAllAsync(x => true);

            //course.GetAwaiter().GetResult();

            //System.Console.ReadKey();
        }
    }
}
