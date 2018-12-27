using System;
using System.Linq;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.Commands;
using SvishtovHighSchool.Domain.Domain;
using SvishtovHighSchool.Domain.Events;
using SvishtovHighSchool.Domain.Handlers;
using SvishtovHighSchool.Domain.Handlers.Commands;
using SvishtovHighSchool.Domain.Handlers.Events;

namespace SvishtovHighSchool.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bus = new FakeBus();

            var storage = new EventStore(bus);

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

            var readModelFacade = new ReadModelFacade();
            var result = readModelFacade.GetInventoryItems();

            var changeCoursName = new ChangeCourseName(courseId, "Chemestry", 0);

            bus.Send(changeCoursName);

            result = readModelFacade.GetInventoryItems();

            var changeCoursName1 = new ChangeCourseName(courseId, "Ankk", 1);

            bus.Send(changeCoursName1);

            result = readModelFacade.GetInventoryItems();
        }
    }
}
