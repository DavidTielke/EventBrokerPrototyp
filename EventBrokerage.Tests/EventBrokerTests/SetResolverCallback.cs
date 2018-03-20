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
        public void SetResolverCallback_NullAsCallback_ArgumentNullException()
        {
            _broker
                .Invoking(b => b.SetResolverCallback(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void SetResolverCallback_NormalCallback_CallbackIsStored()
        {
            var wasCalled = false;
            Func<Type, object> resolver = t =>
            {
                wasCalled = true;
                return new TestHandler();
            };
            _broker.Subscribe<TestHandler, TestMessage>((handler, msg) => msg.ToString());

            _broker.SetResolverCallback(resolver);
            _broker.Raise(new TestMessage());

            wasCalled.Should().BeTrue("the resolver callback was called");
        }
    }
}
