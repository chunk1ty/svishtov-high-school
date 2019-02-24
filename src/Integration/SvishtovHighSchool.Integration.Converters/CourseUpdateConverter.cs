﻿using Ankk.Models;
using Google.Protobuf;
using SvishtovHighSchool.Domain.CourseModule;

namespace SvishtovHighSchool.Integration.Converters
{
    public class CourseUpdateConverter : IMessageConverter
    {
        public IMessage ToPayload(object @event)
        {
            var s = (CourseNameChangedEvent) @event;

            return new CourseUpdated
            {
                Id = s.AggregateId.ToString(),
                Name = s.NewName
            };
        }
    }
}