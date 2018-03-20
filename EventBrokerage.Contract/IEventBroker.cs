﻿using System;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Contract
{
    public interface IEventBroker
    {
        void Subscribe<TMessage>(Func<TMessage, bool> filter, Action<TMessage> handler);
        void Subscribe<TMessage>(Action<TMessage> handler);
        void Raise(object message);
        void Subscribe<THandler, TMessage>(Action<THandler, TMessage> handler);
        void Subscribe<THandler, TMessage>(Func<TMessage, bool> filter, Action<THandler, TMessage> handler);
        void SetResolverCallback(Func<Type, object> resolverCallback);
    }
}
