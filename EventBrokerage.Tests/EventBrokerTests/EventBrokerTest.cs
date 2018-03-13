using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DavidTielke.MBH.CrossCutting.EventBrokerage.Tests.EventBrokerTests
{
    [TestClass]
    public partial class EventBrokerTests
    {
        private EventBroker _broker;

        [TestInitialize]
        public void TestInitialize()
        {
            _broker = new EventBroker();
        }
    }
}
