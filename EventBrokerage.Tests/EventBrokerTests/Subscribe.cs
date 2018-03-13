using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidTielke.MBH.CrossCutting.EventBrokerage.Contract.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Tests.EventBrokerTests
{
    public partial class EventBrokerTests
    {
        [TestMethod]
        public void Subscribe_NullAsHandler_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.Subscribe<TestMessage>(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Subscribe_NoSubscribtionsButOneAdd_OneSubscription()
        {
            Action<TestMessage> handler = msg => msg.Message = "Test";

            _broker.Subscribe(handler);

            _broker.AmountSubscriptions
                .Should()
                .Be(1, "one subscription was added.");
        }

        [TestMethod]
        public void Subscribe_NoSubscribtionsButTwoAdds_TwoSubscription()
        {
            Action<TestMessage> handler1 = msg => msg.Message = "Test";
            Action<TestMessage> handler2 = msg => msg.Message = "Test";

            _broker.Subscribe(handler1);
            _broker.Subscribe(handler2);

            _broker.AmountSubscriptions
                .Should()
                .Be(2, "two subscription was added.");
        }

        [TestMethod]
        public void Subscribe_TwoEqualHandlerAdded_DuplicatedHandlerException()
        {
            Action<TestMessage> handler = msg => msg.Message = "Test";
            _broker.Subscribe(handler);

            _broker
                .Invoking(b => b.Subscribe(handler))
                .Should()
                .Throw<DuplicatedHandlerException>();
        }

        // Todo: TC Reihenfolge testen
    }
}
