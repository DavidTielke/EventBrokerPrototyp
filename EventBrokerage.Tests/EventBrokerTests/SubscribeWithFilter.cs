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
        public void SubscribeWithFilter_NullAsHandler_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.Subscribe<TestMessage>(msg => msg.Message != null, null))
                .Should()
                .Throw<ArgumentNullException>();
        }
        
        [TestMethod]
        public void SubscribeWithFilter_NullAsFilter_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.Subscribe<TestMessage>(null, msg => msg.Message = "Foo"))
                .Should()
                .Throw<ArgumentNullException>();
        }
        
        [TestMethod]
        public void SubscribeWithFilter_FilterIsTrue_HandlerWasCalled()
        {
            var wasCalled = false;
            Func<TestMessage, bool> filter = msg => msg.Message == "Test";
            Action<TestMessage> handler = msg => wasCalled = true;

            _broker.Subscribe(filter, handler);
            _broker.Raise(new TestMessage {Message = "Test"});

            wasCalled.Should().BeTrue("the filter was true for the passed maessage");
        }


        [TestMethod]
        public void SubscribeWithFilter_FilterIsFalse_HandlerWasNotCalled()
        {
            var wasCalled = false;
            Func<TestMessage, bool> filter = msg => msg.Message == "Not Passed";
            Action<TestMessage> handler = msg => wasCalled = true;

            _broker.Subscribe(filter, handler);
            _broker.Raise(new TestMessage { Message = "Test" });

            wasCalled.Should().BeFalse("the filter was false for the passed maessage");
        }
    }
}
