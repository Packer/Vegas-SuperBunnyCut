namespace SuperBunnyCut
{
    partial class AudioSplitter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioSplitter));
            this.processAudio = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.volThreshold = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pauseTimeMin = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mediaSampleBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SplitPointNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauseTimeMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitPointNum)).BeginInit();
            this.SuspendLayout();
            // 
            // processAudio
            // 
            this.processAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.processAudio.BackColor = System.Drawing.SystemColors.Control;
            this.processAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processAudio.Location = new System.Drawing.Point(224, 169);
            this.processAudio.Name = "processAudio";
            this.processAudio.Size = new System.Drawing.Size(141, 39);
            this.processAudio.TabIndex = 0;
            this.processAudio.Text = "Process";
            this.processAudio.UseVisualStyleBackColor = false;
            this.processAudio.Click += new System.EventHandler(this.processAudio_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 196);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("9px3bus", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Super Bunny Cut";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.Location = new System.Drawing.Point(404, 170);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(141, 39);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // volThreshold
            // 
            this.volThreshold.AllowDrop = true;
            this.volThreshold.DecimalPlaces = 1;
            this.volThreshold.Location = new System.Drawing.Point(434, 33);
            this.volThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.volThreshold.Name = "volThreshold";
            this.volThreshold.Size = new System.Drawing.Size(70, 20);
            this.volThreshold.TabIndex = 4;
            this.volThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.volThreshold.ValueChanged += new System.EventHandler(this.volThreshold_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("BigBlue TerminalPlus", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(221, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Volume Threshold:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("BigBlue TerminalPlus", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(221, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Minimum Silent Time:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // numericUpDown1
            // 
            this.pauseTimeMin.AllowDrop = true;
            this.pauseTimeMin.DecimalPlaces = 1;
            this.pauseTimeMin.Location = new System.Drawing.Point(434, 61);
            this.pauseTimeMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.pauseTimeMin.Name = "numericUpDown1";
            this.pauseTimeMin.Size = new System.Drawing.Size(70, 20);
            this.pauseTimeMin.TabIndex = 6;
            this.pauseTimeMin.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(509, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "%";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(504, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "(sec)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // mediaSampleBtn
            // 
            this.mediaSampleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mediaSampleBtn.BackColor = System.Drawing.SystemColors.Control;
            this.mediaSampleBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediaSampleBtn.Location = new System.Drawing.Point(405, 125);
            this.mediaSampleBtn.Name = "mediaSampleBtn";
            this.mediaSampleBtn.Size = new System.Drawing.Size(141, 39);
            this.mediaSampleBtn.TabIndex = 10;
            this.mediaSampleBtn.Text = "Sample Media";
            this.mediaSampleBtn.UseVisualStyleBackColor = false;
            this.mediaSampleBtn.Click += new System.EventHandler(this.mediaSampleBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("BigBlue TerminalPlus", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(221, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "Split Point:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(509, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "%";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SplitPointNum
            // 
            this.SplitPointNum.AllowDrop = true;
            this.SplitPointNum.DecimalPlaces = 1;
            this.SplitPointNum.Location = new System.Drawing.Point(434, 89);
            this.SplitPointNum.Name = "SplitPointNum";
            this.SplitPointNum.Size = new System.Drawing.Size(70, 20);
            this.SplitPointNum.TabIndex = 12;
            this.SplitPointNum.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.SplitPointNum.ValueChanged += new System.EventHandler(this.SplitPointNum_ValueChanged);
            // 
            // AudioSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 215);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SplitPointNum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.mediaSampleBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pauseTimeMin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.volThreshold);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.processAudio);
            this.Name = "AudioSplitter";
            this.Text = "Super Bunny Cut";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauseTimeMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitPointNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button processAudio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.NumericUpDown volThreshold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown pauseTimeMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button mediaSampleBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown SplitPointNum;
    }
}