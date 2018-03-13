using System;
using System.Runtime.Serialization;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Contract.Exceptions
{
    [Serializable]
    public class EventBrokerageException : Exception
    {
        public EventBrokerageException()
        {
        }

        public EventBrokerageException(string message) : base(message)
        {
        }

        public EventBrokerageException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EventBrokerageException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
