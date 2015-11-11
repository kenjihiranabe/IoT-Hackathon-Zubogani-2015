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
        public MainDialog Dialog { get; set; }

        private object lockObject = new object();

        private SensorMsg currentMsg = null;
        private Timer timer;

        public void UpdateSensorMessage(SensorMsg msg)
        {
            lock (lockObject)
            {
                currentMsg = msg;
            }
        }

        public void Run()
        {
            TimerCallback timerDelegate = new TimerCallback(uploadPeriodic);
            timer = new Timer(timerDelegate, null, 0, 5000);
        }

        private void uploadPeriodic(object obj)
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
                return;
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
            sendRequest(queryStr);
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
