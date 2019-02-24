using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SvishtovHighSchool.Domain;
using SvishtovHighSchool.Domain.CourseModule;
using SvishtovHighSchool.Infrastructure;
using SvishtovHighSchool.ReadModel;
using SvishtovHighSchool.ReadModel.Contracts;
using SvishtovHighSchool.ReadModel.Entities;

namespace SvishtovHighSchool.Application.Handlers.Events
{
    public class CourseCreatedHandler : INotificationHandler<CourseCreatedEvent>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISender _sender;

        public CourseCreatedHandler(ICourseRepository courseRepository /*ISender sender*/)
        {
            _courseRepository = courseRepository;
            //_sender = sender;
        }

        //public void Handle(CourseCreatedEvent @event)
        //{
        //    var course = new CourseDto(@event.AggregateId.ToString(), @event.Name);

        //    try
        //    {
        //        _courseRepository.AddAsync(course).GetAwaiter().GetResult();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
            

        //    _sender.SendMessagesAsync<CourseCreatedEvent>(@event).GetAwaiter().GetResult();
        //}

        public async Task Handle(CourseCreatedEvent notification, CancellationToken cancellationToken)
        {
            var course = new CourseDto(notification.AggregateId.ToString(), notification.Name);

            try
            {
                _courseRepository.AddAsync(course).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            //_sender.SendMessagesAsync<CourseCreatedEvent>(notification).GetAwaiter().GetResult();
        }
    }
}
