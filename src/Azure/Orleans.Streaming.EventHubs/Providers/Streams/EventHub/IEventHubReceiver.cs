﻿using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.ServiceBus.Providers
{
    /// <summary>
    /// Abstraction on EventhubReceiver class, used to configure EventHubReceiver class in EventhubAdapterReceiver,
    /// also used to configure EHGeneratorReceiver in EventHubAdapterReceiver for testing purpose
    /// </summary>
    public interface IEventHubReceiver
    {
        /// <summary>
        /// Send an async message to the partition asking for more messages
        /// </summary>
        /// <param name="maxCount">Max amount of message which should be delivered in this request</param>
        /// <param name="waitTime">Wait time of this request</param>
        /// <returns></returns>
        Task<IEnumerable<EventData>> ReceiveAsync(int maxCount, TimeSpan waitTime);

        /// <summary>
        /// Send a clean up message
        /// </summary>
        /// <returns></returns>
        Task CloseAsync();
    }

    /// <summary>
    /// pass through decorator class for EventHubReceiver
    /// </summary>
    internal class EventHubReceiverProxy: IEventHubReceiver
    {
        private PartitionReceiver receiver;

        public EventHubReceiverProxy(PartitionReceiver receiver)
        {
            this.receiver = receiver;
        }

        public Task<IEnumerable<EventData>> ReceiveAsync(int maxCount, TimeSpan waitTime)
        {
            return this.receiver.ReceiveAsync(maxCount, waitTime);
        }

        public Task CloseAsync()
        {
            return this.receiver.CloseAsync();
        }
    }
}
