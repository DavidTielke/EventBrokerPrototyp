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
        public void Raise_MessageIsNull_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.Raise(null))
                .Should()
                .Throw<ArgumentNullException>("null as a message is not allowed");
        }

        [TestMethod]
        public void Raise_MessageHasSubscriber_SubscriberWasCalled()
        {
            var isCalled = false;
            Action<TestMessage> handler = msg => isCalled = true;
            _broker.Subscribe(handler);
            
            _broker.Raise(new TestMessage());

            isCalled.Should().BeTrue("the raised message was subscribed for that handler");
        }

        [TestMethod]
        public void Raise_MessageHasTwoSubscribers_SubscribersWereCalled()
        {
            var isCalled1 = false;
            var isCalled2 = false;
            Action<TestMessage> handler1 = msg => isCalled1 = true;
            Action<TestMessage> handler2 = msg => isCalled2 = true;
            _broker.Subscribe(handler1);
            _broker.Subscribe(handler2);

            _broker.Raise(new TestMessage());

            isCalled1.Should().BeTrue("the raised message was subscribed for that handler");
            isCalled2.Should().BeTrue("the raised message was subscribed for that handler");
        }

        [TestMethod]
        public void Raise_SubscriptionOrderisOneTwo_HandlerOrderIsOneTwo()
        {
            var order = "";
            Action<TestMessage> handler1 = msg => order +="One";
            Action<TestMessage> handler2 = msg => order += "Two";
            _broker.Subscribe(handler1);
            _broker.Subscribe(handler2);

            _broker.Raise(new TestMessage());

            order.Should().Be("OneTwo", "the subscribe order was one two");
        }

        // Todo: TC Logging bei Exception

        [TestMethod]
        public void Raise_SubscriberRaisesException_SubscribersWereCalled()
        {
            Action<TestMessage> handler = msg => throw new Exception();
            _broker.Subscribe(handler);

            _broker.Raise(new TestMessage());
        }

        [TestMethod]
        public void Raise_FirstSubscriberHandlerThrowsException_SecondSubscriberIsCalled()
        {
            var isCalled2 = false;
            Action<TestMessage> handler1 = msg => throw new Exception();
            Action<TestMessage> handler2 = msg => isCalled2 = true;
            _broker.Subscribe(handler1);
            _broker.Subscribe(handler2);

            _broker.Raise(new TestMessage());

            isCalled2.Should().BeTrue("the raised message was subscribed for that handler");
        }

        [TestMethod]
        public void Raise_FirstSubscriberFilterThrowsException_SecondSubscriberIsCalled()
        {
            var isCalled2 = false;
            Action<TestMessage> handler1 = msg => msg.Message = "Nothing";
            Func<TestMessage, bool> filter1 = msg => throw new Exception();
            Action<TestMessage> handler2 = msg => isCalled2 = true;
            _broker.Subscribe(filter1, handler1);
            _broker.Subscribe(handler2);

            _broker.Raise(new TestMessage());

            isCalled2.Should().BeTrue("the raised message was subscribed for that handler");
        }


        [TestMethod]
        public void Raise_MessageHasNoSubscriber_NoError()
        {
            _broker.Raise(new TestMessage());
        }
    }
}
