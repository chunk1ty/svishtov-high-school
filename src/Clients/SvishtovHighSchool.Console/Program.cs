using System;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Domain.Handlers;

namespace SvishtovHighSchool.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bus = new FakeBus();

            var storage = new EventStore(bus);

            var rep = new Repository<Course>(storage);
            var courseCreater = new CourseCreaterHandler(rep);
            bus.RegisterHandler<CourseCreateCommand>(courseCreater.Handle);

            var courseCreatedHandler = new CourseCreatedHandler();
            bus.RegisterHandler<CourseCreated>(courseCreatedHandler.Handle);

            var courseId = Guid.NewGuid();
            var courseName = "Math";

            var courseCreateCommand = new CourseCreateCommand(courseId, courseName);

            bus.Send(courseCreateCommand);

            var readModelFacade = new ReadModelFacade();

            var result = readModelFacade.GetInventoryItems();
        }
    }
}
