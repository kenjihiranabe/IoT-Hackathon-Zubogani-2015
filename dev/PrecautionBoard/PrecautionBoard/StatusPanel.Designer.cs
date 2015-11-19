namespace PrecautionBoard
{
    partial class StatusPanel
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusPanel));
            this.txtTemperature = new System.Windows.Forms.Label();
            this.txtHumidity = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTemperature
            // 
            this.txtTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemperature.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemperature.ForeColor = System.Drawing.Color.DimGray;
            this.txtTemperature.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtTemperature.Location = new System.Drawing.Point(45, 5);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(80, 24);
            this.txtTemperature.TabIndex = 0;
            this.txtTemperature.Text = "22.5℃";
            // 
            // txtHumidity
            // 
            this.txtHumidity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHumidity.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHumidity.ForeColor = System.Drawing.Color.DimGray;
            this.txtHumidity.Location = new System.Drawing.Point(45, 30);
            this.txtHumidity.Name = "txtHumidity";
            this.txtHumidity.Size = new System.Drawing.Size(80, 24);
            this.txtHumidity.TabIndex = 1;
            this.txtHumidity.Text = "70%";
            this.txtHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("VirusNormal")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // StatusPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtHumidity);
            this.Controls.Add(this.txtTemperature);
            this.Name = "StatusPanel";
            this.Size = new System.Drawing.Size(130, 60);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtTemperature;
        private System.Windows.Forms.Label txtHumidity;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
