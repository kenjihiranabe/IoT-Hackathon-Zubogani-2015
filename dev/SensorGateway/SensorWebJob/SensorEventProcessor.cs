using Microsoft.Azure.NotificationHubs;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SensorWebJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SensorWebJob
{
    public class SensorEventProcessor : IEventProcessor
    {
        private const string APP_URL_MOBILE_SERVICES = "https://zubo-sensor.azure-mobile.net/";
        private const string APP_KEY_MOBILE_SERVICES = "KKHtgfGVyeqINKrecqdwlJBZdOGtkt88";

        //private HttpClient client;
        private CloudTable sensorLogTable;

        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            return Task.FromResult<object>(null);
        }

        public Task OpenAsync(PartitionContext context)
        {
            /*client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", APP_KEY_MOBILE_SERVICES);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return Task.FromResult<object>(null);*/

            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var tableClient = storageAccount.CreateCloudTableClient();
            sensorLogTable = tableClient.GetTableReference("SensorLog");
            //sensorLogTable = tableClient.GetTableReference("SensorLog" + context.Lease.PartitionId);
            return sensorLogTable.CreateIfNotExistsAsync();
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (EventData msg in messages)
            {
                if (msg.EnqueuedTimeUtc < DateTime.Today)
                {
                    //continue;
                }
                SensorLog sensorLog = new SensorLog(context.EventHubPath, msg.Offset, msg.EnqueuedTimeUtc, context.Lease.PartitionId);
                
                foreach (string propKey in msg.Properties.Keys)
                {
                    switch (propKey)
                    {
                        case "deviceId":
                            // DeviceId を PartitionKeyとして使う
                            sensorLog.PartitionKey = (string)msg.Properties[propKey];
                            break;
                        case "pressure":
                            sensorLog.Pressure = (float)msg.Properties[propKey];
                            break;
                        case "humidity":
                            sensorLog.Humidity = (float)msg.Properties[propKey];
                            break;
                        case "temperature":
                            sensorLog.Temperature = (float)msg.Properties[propKey];
                            break;
                        case "accelerationX":
                            sensorLog.AccelerationX = (int)msg.Properties[propKey];
                            break;
                        case "accelerationY":
                            sensorLog.AccelerationY = (int)msg.Properties[propKey];
                            break;
                        case "accelerationZ":
                            sensorLog.AccelerationZ = (int)msg.Properties[propKey];
                            break;
                        case "gyroX":
                            sensorLog.GyroX = (int)msg.Properties[propKey];
                            break;
                        case "gyroY":
                            sensorLog.GyroY = (int)msg.Properties[propKey];
                            break;
                        case "gyroZ":
                            sensorLog.GyroZ = (int)msg.Properties[propKey];
                            break;
                        case "magnetoX":
                            sensorLog.MagnetoX = (int)msg.Properties[propKey];
                            break;
                        case "magnetoY":
                            sensorLog.MagnetoY = (int)msg.Properties[propKey];
                            break;
                        case "magnetoZ":
                            sensorLog.MagnetoZ = (int)msg.Properties[propKey];
                            break;
                    }
                }
                sensorLogTable.ExecuteAsync(TableOperation.Insert(sensorLog));
                checkTodoSendData(sensorLog);

                /*string uriStr = createUriToMobileService(sensorLog);
                var request = new HttpRequestMessage(new HttpMethod("POST"), uriStr);
                client.SendAsync(request);*/
            }
            return Task.FromResult<object>(null);
        }

        private async void notifyTest()
        {
            NotificationOutcome outcome = null;
            var notif = "{\"data\":{\"message\":\"notify test\", \"floor\":\"7\", \"room\":\"小会議室\"}}";
            outcome = await Notifications.Instance.Hub.SendGcmNativeNotificationAsync(notif);
            if (outcome != null)
            {
                if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                    (outcome.State == NotificationOutcomeState.Unknown)))
                {
                    // OK
                    System.Console.WriteLine("Outcome OK");
                } else
                {
                    System.Console.WriteLine("Outcome Unknown");
                }
            } else
            {
                System.Console.WriteLine("Outcome Null");
            }
        }

        private void checkTodoSendData(SensorLog sensorLog)
        {
            if (sensorLog.MagnetoX == 167
                && sensorLog.MagnetoY == -709
                && sensorLog.MagnetoZ == 380
                && sensorLog.AccelerationX == 1311
                && sensorLog.AccelerationY == -119
                && sensorLog.AccelerationZ == -433)
            {
                notifyTest();
            }
        }
        
        private string createUriToMobileService(SensorLog sensorLog)
        {
            var uriStr = APP_URL_MOBILE_SERVICES + "tables/SensorLog"
                + "?deviceId=" + sensorLog.DeviceId
                + "&pressure=" + sensorLog.Pressure
                + "&humidity=" + sensorLog.Humidity
                + "&temperature=" + sensorLog.Temperature
                + "&magneto_x=" + sensorLog.MagnetoX
                + "&magneto_y=" + sensorLog.MagnetoY
                + "&magneto_z=" + sensorLog.MagnetoZ
                + "&acceleration_x=" + sensorLog.AccelerationX
                + "&acceleration_y=" + sensorLog.AccelerationY
                + "&acceleration_z=" + sensorLog.AccelerationZ
                + "&gyro_x=" + sensorLog.GyroX
                + "&gyro_y=" + sensorLog.GyroY
                + "&gyro_z=" + sensorLog.GyroZ;
            return uriStr;
        }
    }

    public class SensorLog : TableEntity
    {
        public string DeviceId { get; set; }        // デバイスID
        public DateTime UploadTime { get; set; }    // アップロード時間
        public double Pressure { get; set; }        // 気圧
        public double Humidity { get; set; }        // 湿度
        public double Temperature { get; set; }     // 気温
        public int MagnetoX { get; set; }           // 磁力X
        public int MagnetoY { get; set; }           // 磁力Y
        public int MagnetoZ { get; set; }           // 磁力Z
        public int AccelerationX { get; set; }      // 加速度X
        public int AccelerationY { get; set; }      // 加速度Y
        public int AccelerationZ { get; set; }      // 加速度Z
        public int GyroX { get; set; }              // ジャイロX
        public int GyroY { get; set; }              // ジャイロY
        public int GyroZ { get; set; }              // ジャイロZ

        public SensorLog(string eventHubName, string offset, DateTime enqueueTime)
        {
            PartitionKey = eventHubName;
            RowKey = eventHubName + "_" + offset;

            // 何故か動かない
            //TimeZoneInfo tokyoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            //UploadTime = TimeZoneInfo.ConvertTime(enqueueTime, TimeZoneInfo.Local, tokyoTimeZone);
            UploadTime = enqueueTime;
        }

        public SensorLog(string eventHubName, string offset, DateTime enqueueTime, string partitionId)
        {
            PartitionKey = eventHubName;
            RowKey = eventHubName + "_" + partitionId + "_" + offset;
            UploadTime = enqueueTime;
        }
    }
}
