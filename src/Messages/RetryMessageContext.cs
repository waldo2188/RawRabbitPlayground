using System;
using RawRabbit.Common;
using RawRabbit.Enrichers.MessageContext.Context;

namespace Messages
{
    /// <summary>
    /// RetryMessageContext
    /// </summary>
    public class RetryMessageContext : IMessageContext
    {
        /// <summary>
        /// GlobalRequestId
        /// </summary>
        public Guid GlobalRequestId { get; set; }

        /// <summary>
        /// RetryInformation
        /// </summary>
        public RetryInformation RetryInfo { get; set; }
    }
}
