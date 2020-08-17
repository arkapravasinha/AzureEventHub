using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureEventHub.Receiver
{
    class Program
    {
        private const string Storage_Account_Connection_String = "DefaultEndpointsProtocol=https;AccountName=eventhubhost;AccountKey=ZkrZhaDlAYnzfKdDZxTmcwbCdBn1EDUWshueZX803fK3uw3svOYHYBb/6OxTKw76I0kHbqXRnee3yLBgvilRUQ==;EndpointSuffix=core.windows.net";
        private const string Storage_Container_Name = "hub1data";
        private const string Event_Hub_Connection_string = "Endpoint=sb://arkaeventhubtest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=e6cfQ6SSLZbVKJXceLViilvPs4pQnc9fXxQOMoUapR8=";
        private const string Event_Hub_Name = "hub1";
        static void Main(string[] args)
        {
            MainAsyncTask().GetAwaiter().GetResult();
        }

        static async Task MainAsyncTask()
        {
            Console.WriteLine("Registering event processor");
            var eventProcessorhost = new EventProcessorHost
                (
                Event_Hub_Name,
                PartitionReceiver.DefaultConsumerGroupName,
                Event_Hub_Connection_string,
                Storage_Account_Connection_String,
                Storage_Container_Name
                );

            await eventProcessorhost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving messages, press enter to stop the worker");
            Console.ReadLine();

            await eventProcessorhost.UnregisterEventProcessorAsync();
        }
    }
}
