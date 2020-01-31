using Craftsman.Core.Dependency;
using Craftsman.Core.Dependency.Installers;
using Craftsman.Waiter.Domain;
using Craftsman.Waiter.Domain.MessageConsumer;
using System;

namespace Craftsman.Waiter.ConsumerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var installer = new CoreInstaller();
            installer.Install("Waiter", WebPortalType.WebApplication);

            Console.WriteLine("Hello World!");

            string type = "exchange-data";
            string topicName = "hyman-test-rowdata"; //"hyman-test";// "custom-message";



            using (var scope = IocFactory.Container.BeginLifetimeScope())
            {
                IConsumerService service = IocFactory.CreateObject<IConsumerService>();

                var consumerType = ConsumerType.PersistentData;
                switch (type.ToLower())
                {
                    case "persistentdata":
                    case "persistent-data":
                        consumerType = ConsumerType.PersistentData;
                        break;
                    case "custom":
                        consumerType = ConsumerType.Custom;
                        break;
                    case "exchangedata":
                    case "exchange-data":
                        consumerType = ConsumerType.ExchangeData;
                        break;
                    default:
                        throw new Exception($"未知的消费者类型：{type}");
                }


                var consumer = service.CreateConsumer(consumerType);

                consumer.Run(topicName, $"group-{type}");
            }

        }
    }
}
