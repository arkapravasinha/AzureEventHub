using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureEventHub
{
    class Program
    {
        private static EventHubClient eventHubClient;
        private const string Event_Hub_Connection_string = "Endpoint=sb://arkaeventhubtest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=e6cfQ6SSLZbVKJXceLViilvPs4pQnc9fXxQOMoUapR8=";
        private const string Event_Hub_Name = "hub1";
        static void Main(string[] args)
        {
            MainAsyncTask().GetAwaiter().GetResult();
            
            Console.ReadKey();
        }

        static async Task MainAsyncTask()
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(Event_Hub_Connection_string)
            {
                EntityPath=Event_Hub_Name
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            await SendMessagesToEventHub();

            if (!eventHubClient.IsClosed)
                await eventHubClient.CloseAsync();
        }

        private async static Task SendMessagesToEventHub()
        {
            for (int i = 0; i < 100; i++)   
            {
                try
                {
                    var message = $"Message {i} at {DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss:ms")}";
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                await Task.Delay(10);
            }
        }
    }
}
