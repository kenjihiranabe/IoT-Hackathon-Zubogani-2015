using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
using System.Threading;
using Microsoft.WindowsAzure.Storage;

namespace SensorWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        private const string CS_EVENTHUB = "Endpoint=sb://zubo-sensormsg-ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=jgjG0QM6UWyhM70+T/JwIXBjHKR+e9CspTxfo37gCaQ=";
        private const string CS_MOBILE_SERVICE = "Data Source=zubo.database.windows.net;Initial Catalog=main_db;User ID=khHKJZeUWSLogin_zubo-sensor;Password=mm14mB378034zZ$$;Asynchronous Processing=True;TrustServerCertificate=False;";
        private const string NAME_EVENTHUB = "device";
        private const string NAMESPACE_EVENTHUB = "zubo-sensorMsg-ns";
        private const string STORE_CONSUMER_GROUP_NAME = "storing";

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var program = new Program();
            program.SetupStoring().Wait();
            Thread.Sleep(Timeout.Infinite);
        }

        /*
        async Task SetupStoring()
        {
            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(CS_EVENTHUB, NAME_EVENTHUB);

            var nsManager = NamespaceManager.CreateFromConnectionString(CS_EVENTHUB);
            var ehDesc = await nsManager.GetEventHubAsync(NAME_EVENTHUB);
            var storeCGDesc = await nsManager.CreateConsumerGroupIfNotExistsAsync(NAME_EVENTHUB, STORE_CONSUMER_GROUP_NAME);
            var storeCG = eventHubClient.GetConsumerGroup(STORE_CONSUMER_GROUP_NAME);

            var epHost = new EventProcessorHost(NAMESPACE_EVENTHUB, NAME_EVENTHUB, STORE_CONSUMER_GROUP_NAME, CS_EVENTHUB, CS_MOBILE_SERVICE);
            await epHost.RegisterEventProcessorAsync<SensorEventProcessor>();
        }*/

        async Task SetupStoring()
        {
            SensorEventProcessor.StartedTime = DateTime.UtcNow;

            var storeCS = CloudConfigurationManager.GetSetting("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(storeCS);

            var eventHubCS = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var ehClient = EventHubClient.CreateFromConnectionString(eventHubCS, NAME_EVENTHUB);

            var nsManager = NamespaceManager.CreateFromConnectionString(eventHubCS);
            var ehDesc = await nsManager.GetEventHubAsync(NAME_EVENTHUB);
            var storeCGDesc = await nsManager.CreateConsumerGroupIfNotExistsAsync(NAME_EVENTHUB, STORE_CONSUMER_GROUP_NAME);
            var storeCG = ehClient.GetConsumerGroup(STORE_CONSUMER_GROUP_NAME);

            var epHost = new EventProcessorHost(NAMESPACE_EVENTHUB, NAME_EVENTHUB, STORE_CONSUMER_GROUP_NAME, eventHubCS, storeCS);
            await epHost.RegisterEventProcessorAsync<SensorEventProcessor>();
        }
    }
}
