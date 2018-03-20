using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Contract.Exceptions
{
    [Serializable]
    public class NoResolveCallbackException : EventBrokerageException
    {
        public NoResolveCallbackException()
        {
        }

        public NoResolveCallbackException(string message) : base(message)
        {
        }

        public NoResolveCallbackException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoResolveCallbackException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
