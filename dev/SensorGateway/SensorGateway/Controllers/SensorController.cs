using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SensorGateway.Controllers
{
    public class SensorController : ApiController
    {
        private const string APP_URL_MOBILE_SERVICES = "https://zubo-sensor.azure-mobile.net/";
        private const string APP_KEY_MOBILE_SERVICES = "KKHtgfGVyeqINKrecqdwlJBZdOGtkt88";

        /*
        public IEnumerable<SensorLog> Get(string device_id)
        {
            var queryPairs = this.Request.GetQueryNameValuePairs();
            string dateLong = queryPairs.FirstOrDefault(q => q.Key == "date").Value;

            string storeCS = CloudConfigurationManager.GetSetting("StorageConnectionString");
            CloudStorageAccount storageAccound = CloudStorageAccount.Parse(storeCS);
            CloudTableClient tableClient = storageAccound.CreateCloudTableClient();
            CloudTable sensorLogTable = tableClient.GetTableReference("SensorLog");

            TableQuery<SensorLog> query = null;
            if (dateLong == null)
            {
                query = new TableQuery<SensorLog>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, device_id));
            } else
            {
                long dateBinary = long.Parse(dateLong);
                DateTime data = DateTime.FromBinary(dateBinary);
                query = new TableQuery<SensorLog>().Where(
                    TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, device_id),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("UploadTime", QueryComparisons.GreaterThan, data)));
            }
            var results = sensorLogTable.ExecuteQuery(query);
            var sorted = from t in results orderby t.UploadTime select t;
            return sorted.Select(ent => (SensorLog)ent).ToList();
        }*/

        public IEnumerable<SensorLog> Get(string device_id, int len)
        {
            var queryPairs = this.Request.GetQueryNameValuePairs();
            string dateLong = queryPairs.FirstOrDefault(q => q.Key == "date").Value;

            string storeCS = CloudConfigurationManager.GetSetting("StorageConnectionString");
            CloudStorageAccount storageAccound = CloudStorageAccount.Parse(storeCS);
            CloudTableClient tableClient = storageAccound.CreateCloudTableClient();
            tableClient.DefaultRequestOptions = new TableRequestOptions()
            {
                PayloadFormat = TablePayloadFormat.JsonNoMetadata
            };
            CloudTable sensorLogTable = tableClient.GetTableReference("SensorLog");

            TableQuery<SensorLog> query = null;
            if (dateLong == null)
            {
                query = new TableQuery<SensorLog>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, device_id));
            }
            else
            {
                long dateBinary = long.Parse(dateLong);
                DateTime data = DateTime.FromBinary(dateBinary);
                query = new TableQuery<SensorLog>().Where(
                    TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, device_id),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("UploadTime", QueryComparisons.GreaterThan, data)));
            }
            var results = sensorLogTable.ExecuteQuery(query);
            var sorted = from t in results orderby t.UploadTime select t;
            var list = sorted.Select(ent => (SensorLog)ent).ToList();
            return list.GetRange(list.Count - len, len);
        }

        /*
        public IEnumerable<SensorLog> Get(int accX, int accY, int accZ)
        {
            string storeCS = CloudConfigurationManager.GetSetting("StorageConnectionString");
            CloudStorageAccount storageAccound = CloudStorageAccount.Parse(storeCS);
            CloudTableClient tableClient = storageAccound.CreateCloudTableClient();
            CloudTable sensorLogTable = tableClient.GetTableReference("SensorLog");

            var query = new TableQuery<SensorLog>().Where(
                TableQuery.GenerateFilterConditionForInt("AccelerationX", QueryComparisons.Equal, accX)).Where(
                TableQuery.GenerateFilterConditionForInt("AccelerationY", QueryComparisons.Equal, accY)).Where(
                TableQuery.GenerateFilterConditionForInt("AccelerationZ", QueryComparisons.Equal, accZ));
            var results = sensorLogTable.ExecuteQuery(query).
                Select((ent => (SensorLog)ent)).ToList();
            return results;
        }*/

        public string Get(int accX, int accY, int accZ)
        {
            string storeCS = CloudConfigurationManager.GetSetting("StorageConnectionString");
            CloudStorageAccount storageAccound = CloudStorageAccount.Parse(storeCS);
            CloudTableClient tableClient = storageAccound.CreateCloudTableClient();
            CloudTable sensorLogTable = tableClient.GetTableReference("SensorLog");

            var query = new TableQuery<SensorLog>().Where(
                TableQuery.GenerateFilterConditionForInt("AccelerationX", QueryComparisons.Equal, accX)).Where(
                TableQuery.GenerateFilterConditionForInt("AccelerationY", QueryComparisons.Equal, accY)).Where(
                TableQuery.GenerateFilterConditionForInt("AccelerationZ", QueryComparisons.Equal, accZ));
            var results = sensorLogTable.ExecuteQuery(query).
                Select((ent => (SensorLog)ent)).ToList();
            return results[0].UploadTime.ToString() + " # " + results[0].UploadTime.ToBinary() + " # "
                + results[0].UploadTime.ToLongTimeString();
        }

        /* SQLデータベース受信用
        public string Get()
        {
            var uriStr = APP_URL_MOBILE_SERVICES + "/tables/SensorLog";
            var response = getToMobileServices(uriStr);
            return "success; " + response.Content.ToString();
        }*/

        /* SQLデータベース送信用
        public string Get(string device_id)
        {
            try
            {
                if (device_id == "empty_test")
                {
                    SensorLog emptySensorLog = new SensorLog(device_id);
                    insertSensorLog(emptySensorLog);
                    return "empty stored";
                }
                var queryPairs = this.Request.GetQueryNameValuePairs();

                SensorLog sensorLog = new SensorLog(device_id)
                {
                    Pressure = double.Parse(queryPairs.First(q => q.Key == "pressure").Value),
                    Humidity = double.Parse(queryPairs.First(q => q.Key == "humidity").Value),
                    Temperature = double.Parse(queryPairs.First(q => q.Key == "temperature").Value),
                    MagnetoX = int.Parse(queryPairs.First(q => q.Key == "magneto_x").Value),
                    MagnetoY = int.Parse(queryPairs.First(q => q.Key == "magneto_y").Value),
                    MagnetoZ = int.Parse(queryPairs.First(q => q.Key == "magneto_z").Value),
                    AccelerationX = int.Parse(queryPairs.First(q => q.Key == "acceleration_x").Value),
                    AccelerationY = int.Parse(queryPairs.First(q => q.Key == "acceleration_y").Value),
                    AccelerationZ = int.Parse(queryPairs.First(q => q.Key == "acceleration_z").Value),
                    GyroX = int.Parse(queryPairs.First(q => q.Key == "gyro_x").Value),
                    GyroY = int.Parse(queryPairs.First(q => q.Key == "gyro_y").Value),
                    GyroZ = int.Parse(queryPairs.First(q => q.Key == "gyro_z").Value)
                };
                //insertSensorLog(sensorLog);
                return "stored; " + sendDataToMobileService(sensorLog);
            }
            catch (Exception ex)
            {
                return "error; " + ex.ToString();
            }
        }*/

        private string sendDataToMobileService(SensorLog sensorLog)
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
            var result = postToMobileServices(uriStr);
            return result.ToString();
        }

        private HttpResponseMessage getToMobileServices(string uri)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", APP_KEY_MOBILE_SERVICES);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(new HttpMethod("GET"), uri);
            var result = client.SendAsync(request).Result;
            return result;
        }

        private HttpResponseMessage postToMobileServices(string uri)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", APP_KEY_MOBILE_SERVICES);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(new HttpMethod("POST"), uri);
            var result = client.SendAsync(request).Result;
            return result;
        }

        private void insertSensorLog(SensorLog sensorLog)
        {
            string storeCS = CloudConfigurationManager.GetSetting("StorageConnectionString");
            CloudStorageAccount storageAccound = CloudStorageAccount.Parse(storeCS);
            CloudTableClient tableClient = storageAccound.CreateCloudTableClient();
            CloudTable sensorLogTable = tableClient.GetTableReference("SensorLog");
            sensorLogTable.CreateIfNotExistsAsync().Wait();
                
            sensorLogTable.ExecuteAsync(TableOperation.Insert(sensorLog)).Wait();
            this.StatusCode(HttpStatusCode.OK);
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

        public SensorLog()
        {
        }

        public SensorLog(string deviceId)
        {
            DateTime time = DateTime.Now;
            PartitionKey = deviceId;
            RowKey = time.Ticks.ToString();
            DeviceId = deviceId;

            TimeZoneInfo tokyoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            UploadTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, tokyoTimeZone);
        }
    }
}
