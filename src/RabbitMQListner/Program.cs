using Messages;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RawRabbit.Enrichers.MessageContext.Subscribe;
using System.Globalization;

namespace RabbitMQListner
{
    class Program
    {
        private static IBusClient _client;

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

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


            ListenAsync().GetAwaiter().GetResult();
        }

        private static async Task ListenAsync()
        {
            await _client.SubscribeAsync<EmailLowPriority, RetryMessageContext>((requested, ctx) => ServerValuesAsync(requested, ctx),
                ctx => ctx.UseMessageContext(c => new RetryMessageContext
                {
                    GlobalRequestId = Guid.NewGuid(),
                    RetryInfo = c.GetRetryInformation()
                }));

            var otherstuf = "lkj";
        }

        private static async Task<Acknowledgement> ServerValuesAsync(EmailLowPriority message, RetryMessageContext ctx)
        {
            var aMessage = message;
            var aCtx = ctx;

            await File.ReadAllTextAsync("rawrabbit.json");

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Before Retry --------------------------------");
            return Retry.In(TimeSpan.FromSeconds(10));
        }
    }
}
