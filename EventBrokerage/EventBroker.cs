using System;
using System.Collections.Generic;
using System.Linq;
using DavidTielke.MBH.CrossCutting.EventBrokerage.Contract;
using DavidTielke.MBH.CrossCutting.EventBrokerage.Contract.Exceptions;

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

            var isHandlerAlreadyRegistered = _subscriptions[messageType].Contains(handler);
            if (isHandlerAlreadyRegistered)
            {
                throw new DuplicatedHandlerException("Handler was already registered");
            }

            _subscriptions[messageType].Add(handler);
        }

        public int AmountSubscriptions => _subscriptions.SelectMany(s => s.Value).Count();

        public void Raise(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var messageType = message.GetType();
            var hasHandler = _subscriptions.ContainsKey(messageType) && _subscriptions[messageType].Count > 0;
            if (!hasHandler)
            {
                return;
            }

            var handlers = _subscriptions[messageType];

            foreach (var h in handlers)
            {
                try
                {
                    h.DynamicInvoke(message);
                }
                catch(Exception e)
                {
                    // Todo: Logging im Exceptionfall hinzufügen
                    Console.WriteLine(e);
                }
            }
        }
    }
}
