using System;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Contract
{
    public interface IEventBroker
    {
        void Subscribe<TMessage>(Func<TMessage, bool> filter, Action<TMessage> handler);
        void Subscribe<TMessage>(Action<TMessage> handler);
        int AmountSubscriptions { get; }
        void Raise(object message);
    }
}
