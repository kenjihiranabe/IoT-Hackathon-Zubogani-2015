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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.status2F3R = new PrecautionBoard.StatusPanel();
            this.status2F2R = new PrecautionBoard.StatusPanel();
            this.status2F1R = new PrecautionBoard.StatusPanel();
            this.status1F2R = new PrecautionBoard.StatusPanel();
            this.status1F1R = new PrecautionBoard.StatusPanel();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnStart.Location = new System.Drawing.Point(697, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(88, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Room1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(378, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "Room2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(88, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Room3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(285, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Room4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(565, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Room5";
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
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

