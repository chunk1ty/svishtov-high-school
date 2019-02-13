﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace SvishtovHighSchool.Infrastructure
{
    public interface IHandles<T>
    {
        void Handle(T message);
    }

    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;

    }

    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : DomainEvent;
    }

    public class FakeBus : ICommandSender, IEventPublisher
    {
        private readonly Dictionary<Type, List<Action<IDomainMessage>>> _routes = new Dictionary<Type, List<Action<IDomainMessage>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : IDomainMessage
        {
            List<Action<IDomainMessage>> handlers;

            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IDomainMessage>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }

        public void Send<T>(T command) where T : Command
        {
            List<Action<IDomainMessage>> handlers;

            if (_routes.TryGetValue(typeof(T), out handlers))
            {
                if (handlers.Count != 1)
                {
                    throw new InvalidOperationException("cannot send to more than one handler");
                }

                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }

        public void Publish<T>(T @event) where T : DomainEvent
        {
            List<Action<IDomainMessage>> handlers;

            if (!_routes.TryGetValue(((Object) @event).GetType(), out handlers)) return;

            foreach (var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }
    }
}
