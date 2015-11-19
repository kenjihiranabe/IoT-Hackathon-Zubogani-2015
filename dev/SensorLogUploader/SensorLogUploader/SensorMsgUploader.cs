using Amqp;
using Amqp.Framing;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SensorLogUploader
{
    public class SensorMsgUploader
    {
        private const string CONNECTION_STRING = "Endpoint=sb://zubo-sensormsg-ns.servicebus.windows.net/;SharedAccessKeyName=Root;SharedAccessKey=xcG6tBZ4rwylgW0Nva1Ztp2jvRx/sGrX1hEx2yKCt88=";
        private const string NAME_EVENTHUB = "device";

        public MainDialog Dialog { get; set; }
        private object lockObject = new object();

        private SensorMsg currentMsg = null;
        private Timer timer;
        private EventHubClient eventHubClient;

        public void SetupEventHub()
        {
            eventHubClient = EventHubClient.CreateFromConnectionString(CONNECTION_STRING, NAME_EVENTHUB);
        }

        public void UpdateSensorMessage(SensorMsg msg)
        {
            lock (lockObject)
            {
                currentMsg = msg;
            }
        }


        public void Run()
        {
            TimerCallback timerDelegate = new TimerCallback(sendEventData);
            timer = new Timer(timerDelegate, null, 0, 2000);
        }

        private void sendEventData(object obj)
        {
            string deviceId = null;
            DateTime? uploadTime = null;
            float pressure = 0;
            float humidity = 0;
            float temperature = 0;
            int accelerationX = 0;
            int accelerationY = 0;
            int accelerationZ = 0;
            int gyroX = 0;
            int gyroY = 0;
            int gyroZ = 0;
            int magnetoX = 0;
            int magnetoY = 0;
            int magnetoZ = 0;

            lock (lockObject)
            {
                if (currentMsg != null)
                {
                    pressure = currentMsg.Pressure;
                    humidity = currentMsg.Humidity;
                    temperature = currentMsg.Temperature;
                    accelerationX = currentMsg.AccelerationX;
                    accelerationY = currentMsg.AccelerationY;
                    accelerationZ = currentMsg.AccelerationZ;
                    gyroX = currentMsg.GyroX;
                    gyroY = currentMsg.GyroY;
                    gyroZ = currentMsg.GyroZ;
                    magnetoX = currentMsg.MagnetoX;
                    magnetoY = currentMsg.MagnetoY;
                    magnetoZ = currentMsg.MagnetoZ;
                    uploadTime = DateTime.Now;
                    deviceId = "IKS01A1";
                }
            }
            if (deviceId == null)
            {
                return;
            }

            EventData eventData = new EventData(System.Text.UTF8Encoding.UTF8.GetBytes("Sending Sensor"));
            //eventData.PartitionKey = deviceId;
            eventData.Properties["deviceId"] = deviceId;
            eventData.Properties["uploadTime"] = uploadTime;
            eventData.Properties["pressure"] = pressure;
            eventData.Properties["humidity"] = humidity;
            eventData.Properties["temperature"] = temperature;
            eventData.Properties["accelerationX"] = accelerationX;
            eventData.Properties["accelerationY"] = accelerationY;
            eventData.Properties["accelerationZ"] = accelerationZ;
            eventData.Properties["gyroX"] = gyroX;
            eventData.Properties["gyroY"] = gyroY;
            eventData.Properties["gyroZ"] = gyroZ;
            eventData.Properties["magnetoX"] = magnetoX;
            eventData.Properties["magnetoY"] = magnetoY;
            eventData.Properties["magnetoZ"] = magnetoZ;
            eventHubClient.Send(eventData);
            System.Console.WriteLine("Send EventData");
        }

        public void sendDummmyToEventHub()
        {
            EventData eventData = new EventData(System.Text.UTF8Encoding.UTF8.GetBytes("Sending Dummy Sensor"));
            eventData.Properties["deviceId"] = "dummy";
            eventData.Properties["uploadTime"] = DateTime.Now;
            eventData.Properties["pressure"] = (float)1023;
            eventData.Properties["humidity"] = (float)60.25;
            eventData.Properties["temperature"] = (float)20.52;
            eventData.Properties["accelerationX"] = (int)100;
            eventData.Properties["accelerationY"] = (int)200;
            eventData.Properties["accelerationZ"] = (int)300;
            eventData.Properties["gyroX"] = (int)1000;
            eventData.Properties["gyroY"] = (int)2000;
            eventData.Properties["gyroZ"] = (int)3000;
            eventData.Properties["magnetoX"] = (int)10;
            eventData.Properties["magnetoY"] = (int)20;
            eventData.Properties["magnetoZ"] = (int)30;
            eventHubClient.Send(eventData);
            System.Console.WriteLine("Send Dummy EventData");
        }

        private void uploadPeriodicUriQuery(object obj)
        {
            string queryStr = createQueryStr();
            if (queryStr == null)
            {
                return;
            }
            sendRequest(queryStr);
        }

        private string createQueryStr()
        {
            string deviceId = null;
            float pressure = 0;
            float humidity = 0;
            float temperature = 0;
            int accelerationX = 0;
            int accelerationY = 0;
            int accelerationZ = 0;
            int gyroX = 0;
            int gyroY = 0;
            int gyroZ = 0;
            int magnetoX = 0;
            int magnetoY = 0;
            int magnetoZ = 0;

            lock (lockObject)
            {
                if (currentMsg != null)
                {
                    pressure = currentMsg.Pressure;
                    humidity = currentMsg.Humidity;
                    temperature = currentMsg.Temperature;
                    accelerationX = currentMsg.AccelerationX;
                    accelerationY = currentMsg.AccelerationY;
                    accelerationZ = currentMsg.AccelerationZ;
                    gyroX = currentMsg.GyroX;
                    gyroY = currentMsg.GyroY;
                    gyroZ = currentMsg.GyroZ;
                    magnetoX = currentMsg.MagnetoX;
                    magnetoY = currentMsg.MagnetoY;
                    magnetoZ = currentMsg.MagnetoZ;
                    deviceId = "IKS01A1";
                }
            }
            if (deviceId == null)
            {
                return null;
            }
            string queryStr = "?device_id=" + deviceId
                + "&pressure=" + pressure
                + "&humidity=" + humidity
                + "&temperature=" + temperature
                + "&acceleration_x=" + accelerationX
                + "&acceleration_y=" + accelerationY
                + "&acceleration_z=" + accelerationZ
                + "&gyro_x=" + gyroX
                + "&gyro_y=" + gyroY
                + "&gyro_z=" + gyroZ
                + "&magneto_x=" + magnetoX
                + "&magneto_y=" + magnetoY
                + "&magneto_z=" + magnetoZ;
            return queryStr;
        }

        private void sendRequest(string queryStr)
        {
            HttpWebRequest req = WebRequest.Create("https://zubo-sensor.azurewebsites.net/api/Sensor" + queryStr) as HttpWebRequest;
            req.Method = "GET";
            System.Console.WriteLine(req.Address);

            HttpWebResponse res = req.GetResponse() as HttpWebResponse;
            if (Dialog == null)
            {
                System.Console.WriteLine(res.ToString());
            }
            else
            {
                Dialog.Invoke(new MainDialog.DisplayResponceDelegate(Dialog.DisplayResponce), new Object[] { res });
            }
        }

    }
}
