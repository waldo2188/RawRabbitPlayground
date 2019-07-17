using Messages;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQSender
{
    class Program
    {
        private static IBusClient _client;

        static void Main(string[] args)
        {
            //Thread.Sleep(20000);


            _client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("rawrabbit.json")
                    .Build()
                    .Get<RawRabbitConfiguration>(),
                Plugins = p => p
                    .UseGlobalExecutionId()
                    .UseAttributeRouting()
                    .UseRetryLater()
                    .UseMessageContext<RetryMessageContext>()
            });




            //            while (true)
            //            {
            //                Console.WriteLine("Send a new message ?");
            //                Console.ReadKey();
            Thread.Sleep(4000);
                Console.WriteLine("We send a new message !");


                SendMessage().Wait();
            //}
            
        }

        private static async Task SendMessage()
        {
            await _client.PublishAsync(new EmailLowPriority()
            {
                EmailQueueId = 1,
                SendAtDatetime = DateTime.Now
            });

            var doOtherStuf = "qsdqs";
        }
    }
}
