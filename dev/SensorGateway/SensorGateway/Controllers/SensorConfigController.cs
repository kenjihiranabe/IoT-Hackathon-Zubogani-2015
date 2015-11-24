using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SensorGateway.Controllers
{
    public class SensorConfigController : ApiController
    {
        public IEnumerable<SensorConfig> Get()
        {
            string storeCS = CloudConfigurationManager.GetSetting("StorageConnectionString");
            CloudStorageAccount storageAccound = CloudStorageAccount.Parse(storeCS);
            CloudTableClient tableClient = storageAccound.CreateCloudTableClient();
            tableClient.DefaultRequestOptions = new TableRequestOptions()
            {
                PayloadFormat = TablePayloadFormat.JsonNoMetadata
            };
            CloudTable sensorConfigTable = tableClient.GetTableReference("SensorConfig");

            TableQuery<SensorConfig> query = new TableQuery<SensorConfig>();
            var results = sensorConfigTable.ExecuteQuery(query);
            var list = results.Select(ent => (SensorConfig)ent).ToList();
            return list;
        }
    }

    public class SensorConfig : TableEntity
    {
        public double Threshold { get; set; }   // 閾値
    }
}
