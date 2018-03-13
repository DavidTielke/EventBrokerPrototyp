using System;
using System.Runtime.Serialization;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Contract.Exceptions
{
    [Serializable]
    public class DuplicatedHandlerException : EventBrokerageException
    {
        public DuplicatedHandlerException()
        {
        }

        public DuplicatedHandlerException(string message) : base(message)
        {
        }

        public DuplicatedHandlerException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DuplicatedHandlerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}