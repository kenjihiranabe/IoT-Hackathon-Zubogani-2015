namespace PrecautionBoard
{
    partial class BoardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardForm));
            this.status2F3R = new PrecautionBoard.StatusPanel();
            this.status2F2R = new PrecautionBoard.StatusPanel();
            this.status2F1R = new PrecautionBoard.StatusPanel();
            this.status1F2R = new PrecautionBoard.StatusPanel();
            this.status1F1R = new PrecautionBoard.StatusPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // status2F3R
            // 
            this.status2F3R.BackColor = System.Drawing.Color.MistyRose;
            this.status2F3R.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status2F3R.Location = new System.Drawing.Point(642, 106);
            this.status2F3R.Name = "status2F3R";
            this.status2F3R.Size = new System.Drawing.Size(130, 60);
            this.status2F3R.TabIndex = 4;
            // 
            // status2F2R
            // 
            this.status2F2R.BackColor = System.Drawing.Color.MistyRose;
            this.status2F2R.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status2F2R.Location = new System.Drawing.Point(304, 106);
            this.status2F2R.Name = "status2F2R";
            this.status2F2R.Size = new System.Drawing.Size(130, 60);
            this.status2F2R.TabIndex = 3;
            // 
            // status2F1R
            // 
            this.status2F1R.BackColor = System.Drawing.Color.MistyRose;
            this.status2F1R.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status2F1R.Location = new System.Drawing.Point(131, 106);
            this.status2F1R.Name = "status2F1R";
            this.status2F1R.Size = new System.Drawing.Size(130, 60);
            this.status2F1R.TabIndex = 2;
            // 
            // status1F2R
            // 
            this.status1F2R.BackColor = System.Drawing.Color.MistyRose;
            this.status1F2R.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status1F2R.Location = new System.Drawing.Point(546, 246);
            this.status1F2R.Name = "status1F2R";
            this.status1F2R.Size = new System.Drawing.Size(130, 60);
            this.status1F2R.TabIndex = 1;
            // 
            // status1F1R
            // 
            this.status1F1R.BackColor = System.Drawing.Color.MistyRose;
            this.status1F1R.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.status1F1R.Location = new System.Drawing.Point(221, 246);
            this.status1F1R.Name = "status1F1R";
            this.status1F1R.Size = new System.Drawing.Size(130, 60);
            this.status1F1R.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnStart.Location = new System.Drawing.Point(542, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(623, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 56);
            this.textBox1.TabIndex = 6;
            // 
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.status2F3R);
            this.Controls.Add(this.status2F2R);
            this.Controls.Add(this.status2F1R);
            this.Controls.Add(this.status1F2R);
            this.Controls.Add(this.status1F1R);
            this.DoubleBuffered = true;
            this.Name = "BoardForm";
            this.Text = "風邪を引きやすい季節です．注意しましょう";
            this.Load += new System.EventHandler(this.BoardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusPanel status1F1R;
        private StatusPanel status1F2R;
        private StatusPanel status2F1R;
        private StatusPanel status2F2R;
        private StatusPanel status2F3R;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textBox1;
    }
}

