using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrecautionBoard
{
    public partial class StatusPanel : UserControl
    {
        private ComponentResourceManager resources = new ComponentResourceManager(typeof(StatusPanel));

        public Label TemperatureLabel
        {
            get { return this.txtTemperature; }
        }

        public Label HumidityLabel
        {
            get { return this.txtHumidity; }
        }

        public StatusPanel()
        {
            InitializeComponent();
        }

        public void ModifyVirus(VirusType type)
        {
            switch (type)
            {
                case VirusType.Normal:
                    this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("VirusNormal")));
                    break;
                case VirusType.Warning:
                    this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("VirusWarning")));
                    break;
                case VirusType.Danger:
                    this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("VirusDanger")));
                    break;
            }
        }
    }

    public enum VirusType
    {
        Normal,
        Warning,
        Danger
    }
}
