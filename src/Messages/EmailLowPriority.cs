using RawRabbit.Enrichers.Attributes;
using RawRabbit.Configuration.Exchange;
using System;
using RawRabbit.Common;

namespace Messages
{
    /// <summary>
    /// To be used for emails requiring a not really immediate sending
    /// </summary>
    [Queue(Name = "q-email_low_priority", AutoDelete = false, Durable = true)]
    [Exchange(Name = "e-emailing", Type = ExchangeType.Direct)]
    [Routing(RoutingKey = "email_low_priority", AutoAck = false, PrefetchCount = 2)]
    public class EmailLowPriority
    {
        ///<inheritdoc />
        public int EmailQueueId { get; set; }
        ///<inheritdoc />
        public DateTime? SendAtDatetime { get; set; }
    }
}