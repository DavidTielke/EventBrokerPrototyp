using System;
using System.Collections.Generic;
using System.Linq;
using DavidTielke.MBH.CrossCutting.EventBrokerage.Contract;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage
{
    public class EventBroker : IEventBroker
    {
        private readonly Dictionary<Type, List<Delegate>> _subscriptions;

        public EventBroker()
        {
            _subscriptions = new Dictionary<Type, List<Delegate>>();
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var messageType = typeof(TMessage);

            var messageAlreadyHasSubscribers = _subscriptions.ContainsKey(messageType);
            if (!messageAlreadyHasSubscribers)
            {
                _subscriptions[messageType] = new List<Delegate>();
            }

            _subscriptions[messageType].Add(handler);
        }

        public int AmountSubscriptions => _subscriptions.SelectMany(s => s.Value).Count();
    }
}
