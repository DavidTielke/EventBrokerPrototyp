using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Tests.EventBrokerTests
{
    public partial class EventBrokerTests
    {
        [TestMethod]
        public void SubscribeAndActivate_NullAsHandler_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.Subscribe<TestHandler, TestMessage>(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void SubscribeAndActivate_TypeAndHandlerRegistered_SubscriberCountIsOne()
        {
            _broker.Subscribe<TestHandler, TestMessage>((handler, message) => handler.Foo());

            _broker.AmountSubscriptions.Should().Be(1, "one subscription was registered");
        }
    }
}
