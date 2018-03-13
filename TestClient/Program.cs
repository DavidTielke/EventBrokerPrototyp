using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidTielke.MBH.CrossCutting.EventBrokerage;
using DavidTielke.MBH.CrossCutting.EventBrokerage.Contract;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IEventBroker broker = new EventBroker();

            var billManager = new BillManager(broker);
            var taskManager = new TaskManager();

            broker.Subscribe<NewBillMsg>(msg =>
            {
                var title = msg.Sender + ": " + msg.Value;
                taskManager.Create(title);
            });

            billManager.Create();
        }
    }

    class BillManager
    {
        private readonly IEventBroker _broker;

        public BillManager(IEventBroker broker)
        {
            _broker = broker;
        }

        public void Create()
        {
            Console.WriteLine("Bill created");
            _broker.Raise(new NewBillMsg
            {
                Sender = "XYZ",
                Value = 23.47m
            });
        }
    }

    class NewBillMsg
    {
        public string Sender { get; set; }
        public decimal Value { get; set; }
    }

    class TaskManager
    {
        public void Create(string title)
        {
            Console.WriteLine("Task created");
        }
    }
}
