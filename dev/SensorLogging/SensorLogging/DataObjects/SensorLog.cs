using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensorLogging.DataObjects
{
    public class SensorLog : EntityData
    {
        public string DeviceId { get; set; }        // デバイスID
        public DateTime? UploadTime { get; set; }   // アップロード時間
        public float? Pressure { get; set; }        // 気圧
        public float? Humidity { get; set; }        // 湿度
        public float? Temperature { get; set; }     // 気温
        public int? MagnetoX { get; set; }          // 磁力X
        public int? MagnetoY { get; set; }          // 磁力Y
        public int? MagnetoZ { get; set; }          // 磁力Z
        public int? AccelerationX { get; set; }     // 加速度X
        public int? AccelerationY { get; set; }     // 加速度Y
        public int? AccelerationZ { get; set; }     // 加速度Z
        public int? GyroX { get; set; }             // ジャイロX
        public int? GyroY { get; set; }             // ジャイロY
        public int? GyroZ { get; set; }             // ジャイロZ

        public SensorLog(string deviceId)
        {
            DeviceId = deviceId;

            DateTime time = DateTime.Now;
            TimeZoneInfo tokyoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            UploadTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, tokyoTimeZone);
        }
    }
}
