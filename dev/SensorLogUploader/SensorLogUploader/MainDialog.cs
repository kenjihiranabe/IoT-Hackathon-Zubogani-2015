using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SensorLogUploader
{
    public partial class MainDialog : Form
    {
        private const int LEN_SENSOR_DATA   = 53;
        private const byte MSG_EOF          = 0xF0;
        private const byte MSG_BS           = 0xF1;
        private const byte MSG_BS_EOF       = 0xF2;

        private SerialPort serialPort = null;
        private byte[] readBuf = new byte[LEN_SENSOR_DATA * 2];
        private Queue<byte> queue = new Queue<byte>();
        private SensorMsgUploader uploader;

        private delegate void ReceiveDataDelegate(int len);
        public delegate void DisplayResponceDelegate(HttpWebResponse res);

        public void DisplayResponce(HttpWebResponse res)
        {
            this.txtConsole.AppendText(res + "\n");
        }

        private void outputSerialMessage(DemoSerialMsg msg)
        {
            if (msg.Data.Length < 1)
            {
                // パリティデータなしの不正なメッセージ
                this.txtConsole.AppendText("[Invalid data] Parity data is not exist.\n");
                this.txtConsole.AppendText(msg + "\n");
                return;
            }
            if (CheckDemoParity(msg) == false)
            {
                // パリティチェックエラー
                this.txtConsole.AppendText("[Invalid data] Parity check error.\n");
                this.txtConsole.AppendText(msg + "\n");
                return;
            }

            if (msg.CmdType == 1 && msg.CmdStatus == 8)
            {
                if (msg.Data.Length != 49)
                {
                    this.txtConsole.AppendText("[Invalid data] Data length is " + msg.Data.Length + ".\n");
                    this.txtConsole.AppendText(msg + "\n");
                    return;
                }
                // センサデータ
                SensorMsg sensorMsg = SensorMsg.CreateSensorMessage(msg);
                this.txtConsole.AppendText(sensorMsg + "\n");
                uploader.UpdateSensorMessage(sensorMsg);
                System.Console.WriteLine(sensorMsg.Humidity);
                //System.Console.WriteLine(sensorMsg.Temperature);
            }
            else
            {
                // unknown
                this.txtConsole.AppendText("[Unknown Message]\n");
                this.txtConsole.AppendText(msg + "\n");
            }
        }

        // パリティチェック
        private bool CheckDemoParity(DemoSerialMsg msg)
        {
            byte check = 0;
            byte parity = msg.Data[msg.Data.Length - 1];

            check -= msg.CmdType;
            check -= msg.DevAddr;
            check -= msg.CmdStatus;
            for (int i = 0; i < (msg.Data.Length - 1); i++)
            {
                check -= msg.Data[i];
            }
            if (check == parity)
            {
                return true;
            }
            return false;
        }

        private void ReceiveData(int len)
        {
            bool receivedEof = false;
            for (int i = 0; i < len; i++)
            {
                queue.Enqueue(readBuf[i]);
                if (readBuf[i] == MSG_EOF)
                {
                    receivedEof = true;
                }
                
            }
            if (receivedEof)
            {
                try
                {
                    DemoSerialMsg msg = new DemoSerialMsg();
                    msg.CmdType = dequeueProtocol(queue).Value;
                    msg.DevAddr = dequeueProtocol(queue).Value;
                    msg.CmdStatus = dequeueProtocol(queue).Value;

                    List<byte> bytes = new List<byte>();
                    while (true)
                    {
                        byte? d = dequeueProtocol(queue);
                        if (d == null) break;
                        bytes.Add(d.Value);
                    }
                    msg.Data = bytes.ToArray();
                    outputSerialMessage(msg);

                } catch (InvalidOperationException e)
                {
                    this.txtConsole.AppendText("[Dequeue Invalid] " + e.Message + "\n");
                }
            }
        }

        private byte? dequeueProtocol(Queue<byte> q)
        {
            byte data1, data2;
            data1 = q.Dequeue();
            if (data1 == MSG_EOF)
            {
                return null;
            }
            else if (data1 == MSG_BS)
            {
                data2 = q.Peek();
                if (data2 == MSG_BS_EOF)
                {
                    q.Dequeue();
                    return MSG_EOF;
                }
                else if (data2 == MSG_BS)
                {
                    q.Dequeue();
                    return MSG_BS;
                }
                else
                {
                    return data1;
                }
            }
            else
            {
                return data1;
            }
        }

        public MainDialog()
        {
            InitializeComponent();
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            uploader = new SensorMsgUploader();
            uploader.Dialog = this;
            uploader.Run();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string[] portList = SerialPort.GetPortNames();
            this.comboPorts.Items.Clear();

            foreach (string portName in portList)
            {
                this.comboPorts.Items.Add(portName);
            }
            this.txtConsole.AppendText("シリアルポートの検索が終了しました．\n");
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            uploader.SetupEventHub();
            this.txtConsole.AppendText("EventHubをセットアップしました．\n");
            if (this.comboPorts.SelectedItem == null)
            {
                this.txtConsole.AppendText("ポートが未選択です．\n");
                return;
            }
            string portName = this.comboPorts.SelectedItem.ToString();

            if (serialPort != null && serialPort.IsOpen)
            {
                var oldPortName = serialPort.PortName;
                serialPort.Close();
                serialPort = null;
                this.txtConsole.AppendText(oldPortName + "ポートを切断しました．\n");
            }
            serialPort = new SerialPort(portName, 921600);
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            //serialPort.Encoding = Encoding.Unicode;
            serialPort.ReceivedBytesThreshold = 1;

            try
            {
                serialPort.Open();
                serialPort.DataReceived += SerialPort_DataReceived;
                this.txtConsole.AppendText(portName + "ポートに接続しました．\n");

            } catch (Exception ex)
            {
                this.txtConsole.AppendText("接続失敗: " + ex.Message + "\n");
                return;
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                int len = 0;
                try
                {
                    len = serialPort.Read(readBuf, 0, LEN_SENSOR_DATA * 2);
                } catch
                {
                    return;
                }
                if (len > 0)
                {
                    //System.Console.WriteLine(len);
                    Invoke(new ReceiveDataDelegate(ReceiveData), new Object[] { len });
                }
            }
            else
            {
                this.txtConsole.AppendText("ポートは未接続です．\n");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                var portName = serialPort.PortName;
                serialPort.Close();
                serialPort = null;
                this.txtConsole.AppendText(portName + "ポートを切断しました．\n");
            } else
            {
                this.txtConsole.AppendText("ポートは未接続です．\n");
            }
        }

        private void btnSendByte_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                byte EOF = 240;
                byte[] buf = new byte[3] { 1, 255, EOF };
                serialPort.Write(buf, 0, 3);
                this.txtConsole.AppendText("1バイトを送信しました．\n");
            }
            else
            {
                this.txtConsole.AppendText("ポートは未接続です．\n");
            }
        }

        private void btnSendEmpty_Click(object sender, EventArgs e)
        {
            uploader.sendDummmyToEventHub();
        }
    }
}
