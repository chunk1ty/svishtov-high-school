using System;
using System.Linq;
using EventStore.ClientAPI;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Domain.Handlers;
using SvishtovHighSchool.Domain.Handlers.Commands;
using SvishtovHighSchool.Domain.Handlers.Events;
using SvishtovHighSchool.EventStore;

namespace SvishtovHighSchool.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var connection = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            connection.ConnectAsync().Wait();
            var bus = new FakeBus();

            //var storage = new Domain.EventStore(bus);
            var storage = new EventStoreEventStore(connection);

            var rep = new Repository<Course>(storage);

            // create course 
            var courseCreater = new CourseCreaterHandler(rep);
            bus.RegisterHandler<CreateCourse>(courseCreater.Handle);
            var courseCreatedHandler = new CourseCreatedHandler();
            bus.RegisterHandler<CourseCreated>(courseCreatedHandler.Handle);

            // change course name
            var changeCourseName = new CourseNameChangerHandler(rep);
            bus.RegisterHandler<ChangeCourseName>(changeCourseName.Handle);
            var courseNameChangedHandler = new CourseNameChangedHandler();
            bus.RegisterHandler<CourseNameChanged>(courseNameChangedHandler.Handle);

            var courseId = Guid.NewGuid();
            var courseName = "Math";

            var courseCreateCommand = new CreateCourse(courseId, courseName);

            bus.Send(courseCreateCommand);


            var events = storage.GetEventsByAggregateId(courseId).GetAwaiter().GetResult();

            var readModelFacade = new ReadModelFacade();
            var result = readModelFacade.GetInventoryItems();

            var changeCoursName = new ChangeCourseName(courseId, "Chemestry");

            bus.Send(changeCoursName);

            result = readModelFacade.GetInventoryItems();

            events = storage.GetEventsByAggregateId(courseId).GetAwaiter().GetResult();

            var changeCoursName1 = new ChangeCourseName(courseId, "Ankk");

            bus.Send(changeCoursName1);

            result = readModelFacade.GetInventoryItems();
        }
    }
}
