namespace SensorLogUploader
{
    partial class MainDialog
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.comboPorts = new System.Windows.Forms.ComboBox();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnSendByte = new System.Windows.Forms.Button();
            this.txtSendPeriod = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(196, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(277, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // comboPorts
            // 
            this.comboPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPorts.FormattingEnabled = true;
            this.comboPorts.Location = new System.Drawing.Point(47, 23);
            this.comboPorts.Name = "comboPorts";
            this.comboPorts.Size = new System.Drawing.Size(143, 20);
            this.comboPorts.TabIndex = 2;
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 269);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(657, 168);
            this.txtConsole.TabIndex = 3;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(580, 21);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnSendByte
            // 
            this.btnSendByte.Location = new System.Drawing.Point(429, 23);
            this.btnSendByte.Name = "btnSendByte";
            this.btnSendByte.Size = new System.Drawing.Size(90, 23);
            this.btnSendByte.TabIndex = 5;
            this.btnSendByte.Text = "Send 1 byte";
            this.btnSendByte.UseVisualStyleBackColor = true;
            this.btnSendByte.Click += new System.EventHandler(this.btnSendByte_Click);
            // 
            // txtSendPeriod
            // 
            this.txtSendPeriod.Location = new System.Drawing.Point(47, 61);
            this.txtSendPeriod.Name = "txtSendPeriod";
            this.txtSendPeriod.ReadOnly = true;
            this.txtSendPeriod.Size = new System.Drawing.Size(100, 19);
            this.txtSendPeriod.TabIndex = 6;
            this.txtSendPeriod.Text = "2000";
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 449);
            this.Controls.Add(this.txtSendPeriod);
            this.Controls.Add(this.btnSendByte);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.comboPorts);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSearch);
            this.Name = "MainDialog";
            this.Text = "Sensor Log Upload";
            this.Load += new System.EventHandler(this.MainDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox comboPorts;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnSendByte;
        private System.Windows.Forms.TextBox txtSendPeriod;
    }
}

