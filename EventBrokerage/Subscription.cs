using System;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage
{
    public class Subscription
    {
        public Delegate Filter { get; set; }
        public Delegate Handler { get; set; }

        public Subscription(Delegate filter, Delegate handler)
        {
            Filter = filter;
            Handler = handler;
        }
    }
}