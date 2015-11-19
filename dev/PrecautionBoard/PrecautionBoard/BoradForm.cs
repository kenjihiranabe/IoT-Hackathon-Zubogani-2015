using Codeplex.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrecautionBoard
{
    public partial class BoardForm : Form
    {
        private int count = 0;
        private System.Threading.Timer timer;

        private delegate void ReceiveTempAndHumidityDelegate(double temperature, double humidity);

        public BoardForm()
        {
            InitializeComponent();
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {
            this.status1F1R.TemperatureLabel.Text = "20.7℃";
            this.status1F1R.HumidityLabel.Text = "65%";
            this.status2F1R.TemperatureLabel.Text = "23.0℃";
            this.status2F1R.HumidityLabel.Text = "75%";
            this.status2F2R.TemperatureLabel.Text = "23.3℃";
            this.status2F2R.HumidityLabel.Text = "72%";
            this.status2F3R.TemperatureLabel.Text = "21.8℃";
            this.status2F3R.HumidityLabel.Text = "78%";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            TimerCallback timerDelegate = new TimerCallback(receiveSensorLog);
            timer = new System.Threading.Timer(timerDelegate, null, 0, 2000);

        }

        private void toggleVirus(double temperature, double humidity)
        {
            count = (++count) % 3;
            switch (count)
            {
                case 0:
                    this.status1F2R.ModifyVirus(VirusType.Normal);
                    break;
                case 1:
                    this.status1F2R.ModifyVirus(VirusType.Warning);
                    break;
                case 2:
                    this.status1F2R.ModifyVirus(VirusType.Danger);
                    break;
            }
            this.status1F2R.TemperatureLabel.Text = temperature.ToString("F1") + "℃";
            this.status1F2R.HumidityLabel.Text = (int)humidity + "%";
        }

        private void receiveSensorLog(object obj)
        {
            var query = createQueryStr();
            HttpWebRequest req = WebRequest.Create(query) as HttpWebRequest;
            req.ContentType = "application/json; charset=utf-8";
            req.Method = "GET";

            HttpWebResponse res = req.GetResponse() as HttpWebResponse;
            System.Console.WriteLine(res + " / " + res.StatusCode);

            System.IO.Stream st = res.GetResponseStream();
            StreamReader sr = new StreamReader(st);
            string responseBody = sr.ReadToEnd();
            //System.Console.WriteLine(responseBody);

            var jsonArray = DynamicJson.Parse(@responseBody);
            dynamic json = null;
            try
            {
                json = jsonArray[0];
            } catch (Exception ex)
            {
            }
            if (json != null)
            {
                double temperature = json.Temperature;
                double humidity = json.Humidity;
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    this.Invoke(new ReceiveTempAndHumidityDelegate(toggleVirus), new Object[] { temperature, humidity });
                }
            }
            req.Abort();
        }

        private string createQueryStr()
        {
               var query = "https://zubo-sensor.azurewebsites.net/api/Sensor"
                + "?device_id=IKS01A1"
                + "&len=1";
            return query;
        }
    }
}
