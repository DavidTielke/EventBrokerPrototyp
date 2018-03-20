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
        public void SubscribeActivateAndFilter_FilterIsNull_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.Subscribe<TestHandler, TestMessage>(null, (h,msg) => msg.ToString()))
                .Should()
                .Throw<ArgumentNullException>();
        }
        
        [TestMethod]
        public void SubscribeActivateAndFilter_HandlerIsNull_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.Subscribe<TestHandler, TestMessage>(f => f.Message == "", null))
                .Should()
                .Throw<ArgumentNullException>();
        }
        
        [TestMethod]
        public void SubscribeActivateAndFilter_FilterIsTrue_HandlerIsActivatedAndCalled()
        {
            var isCalled = false;
            _broker.SetResolverCallback(t => new TestHandler());
            _broker.Subscribe<TestHandler, TestMessage>(msg => msg.Message == "Test", (handler, msg) =>
                {
                    isCalled = true;
                });

            _broker.Raise(new TestMessage{Message = "Test"});

            isCalled.Should().BeTrue("the filter was matched");
        }
        
        [TestMethod]
        public void SubscribeActivateAndFilter_FilterIsFalse_HandlerIsNotActivatedAndNotCalled()
        {
            var isCalled = false;
            _broker.SetResolverCallback(t => new TestHandler());
            _broker.Subscribe<TestHandler, TestMessage>(msg => msg.Message == "NoMatch", (handler, msg) =>
            {
                isCalled = true;
            });

            _broker.Raise(new TestMessage { Message = "Test" });

            isCalled.Should().BeFalse("the filter was not matched");
        }
    }
}
