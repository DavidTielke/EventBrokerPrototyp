using System;
using System.Collections.Generic;
using System.Linq;
using DavidTielke.MBH.CrossCutting.EventBrokerage.Contract;
using DavidTielke.MBH.CrossCutting.EventBrokerage.Contract.Exceptions;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage
{
    public class EventBroker : IEventBroker
    {
        private readonly Dictionary<Type, List<Subscription>> _subscriptions;

        public EventBroker()
        {
            _subscriptions = new Dictionary<Type, List<Subscription>>();
        }

        public void Subscribe<THandler, TMessage>(Action<THandler, TMessage> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var subscription = new Subscription(handler)
            {
                HandlerType = typeof(THandler)
            };

            AddSubscription<TMessage>(subscription);
        }

        private void AddSubscription<TMessage>(Subscription subscription)
        {
            var messageType = typeof(TMessage);

            var messageAlreadyHasSubscribers = _subscriptions.ContainsKey(messageType);
            if (!messageAlreadyHasSubscribers)
            {
                _subscriptions[messageType] = new List<Subscription>();
            }

            var isHandlerAlreadyRegistered = _subscriptions[messageType].Any(s => s.Handler == subscription.Handler);
            if (isHandlerAlreadyRegistered)
            {
                throw new DuplicatedHandlerException("Handler was already registered");
            }

            _subscriptions[messageType].Add(subscription);
        }

        public void Subscribe<THandler, TMessage>(Func<TMessage, bool> filter, Action<TMessage> handler)
        {

        }

        public void Subscribe<TMessage>(Func<TMessage, bool> filter, Action<TMessage> handler)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Subscribe(handler);
            _subscriptions[typeof(TMessage)]
                .Single(s => s.Handler == handler)
                .Filter = filter;
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            
            var subscription = new Subscription(handler);

            AddSubscription<TMessage>(subscription);
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

            foreach (var handler in handlers)
            {
                try
                {
                    var isFilterSet = handler.Filter != null;
                    if (isFilterSet)
                    {
                        var isFilterMatched = (bool)handler.Filter.DynamicInvoke(message);
                        if (!isFilterMatched)
                        {
                            continue;
                        } 
                    }

                    handler.Handler.DynamicInvoke(message);
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
