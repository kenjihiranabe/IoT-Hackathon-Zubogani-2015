using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLogUploader
{
    public class SensorMsg : DemoSerialMsg
    {
        public string Timestamp { get; set; }
        public float Pressure { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public int AccelerationX { get; set; }
        public int AccelerationY { get; set; }
        public int AccelerationZ { get; set; }
        public int GyroX { get; set; }
        public int GyroY { get; set; }
        public int GyroZ { get; set; }
        public int MagnetoX { get; set; }
        public int MagnetoY { get; set; }
        public int MagnetoZ { get; set; }

        public bool IsEmpty()
        {
            return (Pressure == 0);
        }

        public override string ToString()
        {
            string str = Timestamp + ","
                + Pressure + ","
                + Temperature + ","
                + Humidity + ","
                + AccelerationX + ","
                + AccelerationY + ","
                + AccelerationZ + ","
                + GyroX + ","
                + GyroY + ","
                + GyroZ + ","
                + MagnetoX + ","
                + MagnetoY + ","
                + MagnetoZ;
            return str;
        }

        public static SensorMsg CreateSensorMessage(DemoSerialMsg msg)
        {
            SensorMsg sensorMsg = new SensorMsg();
            sensorMsg.Timestamp = msg.Data[0] + ":" + msg.Data[1] + ":" + msg.Data[2] + "." + msg.Data[3];

            sensorMsg.Pressure = convertIntToFloat(getInt(msg.Data, 4, 2), getInt(msg.Data, 6, 2));
            sensorMsg.Temperature = convertIntToFloat(getInt(msg.Data, 8, 1), getInt(msg.Data, 9, 1));
            sensorMsg.Humidity = convertIntToFloat(getInt(msg.Data, 10, 1), getInt(msg.Data, 11, 1));

            sensorMsg.AccelerationX = getInt(msg.Data, 12, 4);
            sensorMsg.AccelerationY = getInt(msg.Data, 16, 4);
            sensorMsg.AccelerationZ = getInt(msg.Data, 20, 4);
            sensorMsg.GyroX = getInt(msg.Data, 24, 4);
            sensorMsg.GyroY = getInt(msg.Data, 28, 4);
            sensorMsg.GyroZ = getInt(msg.Data, 32, 4);
            sensorMsg.MagnetoX = getInt(msg.Data, 36, 4);
            sensorMsg.MagnetoY = getInt(msg.Data, 40, 4);
            sensorMsg.MagnetoZ = getInt(msg.Data, 44, 4);

            return sensorMsg;
        }

        private static float convertIntToFloat(int intPart, int fracPart)
        {
            string floatStr = intPart + "." + fracPart;
            return float.Parse(floatStr);
        }

        private static int getInt(byte[] bytes, int startIndex, int len)
        {
            byte[] box = new byte[4] { 0, 0, 0, 0 };
            for (int i = 0; i < len; i++)
            {
                byte d = bytes[startIndex + i];
                box[i] = d;
            }
            return BitConverter.ToInt32(box, 0);
        }
    }

    public class DemoSerialMsg
    {
        public byte CmdType { get; set; }
        public byte DevAddr { get; set; }
        public byte CmdStatus { get; set; }
        public byte[] Data { get; set; }
        public override string ToString()
        {
            string str = "[DemoSerialMsg: " + CmdType + ", " + DevAddr + ", " + CmdStatus + "] => ";
            if (Data != null && Data.Length > 0)
            {
                str += Data[0];
                for (int i = 1; i < Data.Length; i++)
                {
                    str += "," + Data[i];
                }
            }
            else
            {
                str += "Data is empty.";
            }
            return str;
        }
    }
}
