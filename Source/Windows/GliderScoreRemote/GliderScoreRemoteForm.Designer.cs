namespace GliderScoreRemote
{
    partial class GliderScoreRemoteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GliderScoreRemoteForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbPollingFrequency = new System.Windows.Forms.TrackBar();
            this.lPollingFrequencyValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lGliderScoreTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lBroadcastDelay = new System.Windows.Forms.Label();
            this.tbBroadcastDelay = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUDPTimerPort = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lUDPTimerRecdData = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUDPTelemetryPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lUDPTelemetryRecdData = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpStatus = new System.Windows.Forms.TabPage();
            this.tcRemoteStatus = new System.Windows.Forms.TabControl();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.cbTimerComPort = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbPollingFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBroadcastDelay)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpStatus.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Polling frequency (times/second):";
            // 
            // tbPollingFrequency
            // 
            this.tbPollingFrequency.Location = new System.Drawing.Point(370, 33);
            this.tbPollingFrequency.Maximum = 100;
            this.tbPollingFrequency.Minimum = 1;
            this.tbPollingFrequency.Name = "tbPollingFrequency";
            this.tbPollingFrequency.Size = new System.Drawing.Size(230, 45);
            this.tbPollingFrequency.TabIndex = 1;
            this.tbPollingFrequency.TickFrequency = 10;
            this.tbPollingFrequency.Value = 1;
            this.tbPollingFrequency.ValueChanged += new System.EventHandler(this.tbPollingFrequency_ValueChanged);
            // 
            // lPollingFrequencyValue
            // 
            this.lPollingFrequencyValue.AutoSize = true;
            this.lPollingFrequencyValue.Location = new System.Drawing.Point(322, 33);
            this.lPollingFrequencyValue.Name = "lPollingFrequencyValue";
            this.lPollingFrequencyValue.Size = new System.Drawing.Size(20, 24);
            this.lPollingFrequencyValue.TabIndex = 2;
            this.lPollingFrequencyValue.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "GliderScore input";
            // 
            // lGliderScoreTime
            // 
            this.lGliderScoreTime.AutoSize = true;
            this.lGliderScoreTime.Font = new System.Drawing.Font("Monospac821 BT", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lGliderScoreTime.Location = new System.Drawing.Point(25, 32);
            this.lGliderScoreTime.Name = "lGliderScoreTime";
            this.lGliderScoreTime.Size = new System.Drawing.Size(170, 57);
            this.lGliderScoreTime.TabIndex = 4;
            this.lGliderScoreTime.Text = "00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Broadcast Delay (ms):";
            // 
            // lBroadcastDelay
            // 
            this.lBroadcastDelay.AutoSize = true;
            this.lBroadcastDelay.Location = new System.Drawing.Point(326, 100);
            this.lBroadcastDelay.Name = "lBroadcastDelay";
            this.lBroadcastDelay.Size = new System.Drawing.Size(20, 24);
            this.lBroadcastDelay.TabIndex = 6;
            this.lBroadcastDelay.Text = "0";
            // 
            // tbBroadcastDelay
            // 
            this.tbBroadcastDelay.LargeChange = 500;
            this.tbBroadcastDelay.Location = new System.Drawing.Point(370, 100);
            this.tbBroadcastDelay.Maximum = 2500;
            this.tbBroadcastDelay.Name = "tbBroadcastDelay";
            this.tbBroadcastDelay.Size = new System.Drawing.Size(230, 45);
            this.tbBroadcastDelay.SmallChange = 100;
            this.tbBroadcastDelay.TabIndex = 7;
            this.tbBroadcastDelay.TickFrequency = 100;
            this.tbBroadcastDelay.ValueChanged += new System.EventHandler(this.tbBroadcastDelay_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "UDP Timer Port:";
            // 
            // tbUDPTimerPort
            // 
            this.tbUDPTimerPort.BackColor = System.Drawing.SystemColors.Control;
            this.tbUDPTimerPort.Location = new System.Drawing.Point(330, 154);
            this.tbUDPTimerPort.Name = "tbUDPTimerPort";
            this.tbUDPTimerPort.Size = new System.Drawing.Size(100, 29);
            this.tbUDPTimerPort.TabIndex = 9;
            this.tbUDPTimerPort.Text = "5723";
            this.tbUDPTimerPort.TextChanged += new System.EventHandler(this.tbUDPPort_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lUDPTimerRecdData);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(234, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 100);
            this.panel1.TabIndex = 10;
            // 
            // lUDPTimerRecdData
            // 
            this.lUDPTimerRecdData.AutoSize = true;
            this.lUDPTimerRecdData.Font = new System.Drawing.Font("Monospac821 BT", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUDPTimerRecdData.Location = new System.Drawing.Point(33, 37);
            this.lUDPTimerRecdData.Name = "lUDPTimerRecdData";
            this.lUDPTimerRecdData.Size = new System.Drawing.Size(170, 57);
            this.lUDPTimerRecdData.TabIndex = 5;
            this.lUDPTimerRecdData.Text = "00:00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "Display output";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lGliderScoreTime);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(8, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 100);
            this.panel2.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(136, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "UDP Telemetry Port:";
            // 
            // tbUDPTelemetryPort
            // 
            this.tbUDPTelemetryPort.BackColor = System.Drawing.SystemColors.Control;
            this.tbUDPTelemetryPort.Location = new System.Drawing.Point(330, 197);
            this.tbUDPTelemetryPort.Name = "tbUDPTelemetryPort";
            this.tbUDPTelemetryPort.Size = new System.Drawing.Size(100, 29);
            this.tbUDPTelemetryPort.TabIndex = 13;
            this.tbUDPTelemetryPort.Text = "5724";
            this.tbUDPTelemetryPort.TextChanged += new System.EventHandler(this.tbUDPTelemetryPort_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 565);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "Telemetry:";
            // 
            // lUDPTelemetryRecdData
            // 
            this.lUDPTelemetryRecdData.AutoSize = true;
            this.lUDPTelemetryRecdData.Location = new System.Drawing.Point(127, 565);
            this.lUDPTelemetryRecdData.Name = "lUDPTelemetryRecdData";
            this.lUDPTelemetryRecdData.Size = new System.Drawing.Size(205, 24);
            this.lUDPTelemetryRecdData.TabIndex = 15;
            this.lUDPTelemetryRecdData.Text = "<latest telemetry string>";
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpStatus);
            this.tcMain.Controls.Add(this.tpSettings);
            this.tcMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(950, 631);
            this.tcMain.TabIndex = 16;
            // 
            // tpStatus
            // 
            this.tpStatus.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tpStatus.Controls.Add(this.tcRemoteStatus);
            this.tpStatus.Controls.Add(this.panel2);
            this.tpStatus.Controls.Add(this.lUDPTelemetryRecdData);
            this.tpStatus.Controls.Add(this.panel1);
            this.tpStatus.Controls.Add(this.label7);
            this.tpStatus.Location = new System.Drawing.Point(4, 33);
            this.tpStatus.Name = "tpStatus";
            this.tpStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tpStatus.Size = new System.Drawing.Size(942, 594);
            this.tpStatus.TabIndex = 0;
            this.tpStatus.Text = "Status";
            // 
            // tcRemoteStatus
            // 
            this.tcRemoteStatus.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcRemoteStatus.Location = new System.Drawing.Point(8, 114);
            this.tcRemoteStatus.Name = "tcRemoteStatus";
            this.tcRemoteStatus.SelectedIndex = 0;
            this.tcRemoteStatus.Size = new System.Drawing.Size(924, 435);
            this.tcRemoteStatus.TabIndex = 16;
            // 
            // tpSettings
            // 
            this.tpSettings.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tpSettings.Controls.Add(this.cbTimerComPort);
            this.tpSettings.Controls.Add(this.label8);
            this.tpSettings.Controls.Add(this.label1);
            this.tpSettings.Controls.Add(this.tbPollingFrequency);
            this.tpSettings.Controls.Add(this.lPollingFrequencyValue);
            this.tpSettings.Controls.Add(this.tbUDPTelemetryPort);
            this.tpSettings.Controls.Add(this.label3);
            this.tpSettings.Controls.Add(this.label6);
            this.tpSettings.Controls.Add(this.lBroadcastDelay);
            this.tpSettings.Controls.Add(this.tbBroadcastDelay);
            this.tpSettings.Controls.Add(this.label4);
            this.tpSettings.Controls.Add(this.tbUDPTimerPort);
            this.tpSettings.Location = new System.Drawing.Point(4, 33);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(942, 594);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "Settings";
            // 
            // cbTimerComPort
            // 
            this.cbTimerComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimerComPort.FormattingEnabled = true;
            this.cbTimerComPort.Location = new System.Drawing.Point(330, 245);
            this.cbTimerComPort.Name = "cbTimerComPort";
            this.cbTimerComPort.Size = new System.Drawing.Size(270, 32);
            this.cbTimerComPort.TabIndex = 15;
            this.cbTimerComPort.SelectedValueChanged += new System.EventHandler(this.cbTimerComPort_SelectedValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(165, 248);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 24);
            this.label8.TabIndex = 14;
            this.label8.Text = "Timer COM Port:";
            // 
            // GliderScoreRemoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(948, 631);
            this.Controls.Add(this.tcMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "GliderScoreRemoteForm";
            this.Text = "GliderScoreRemote";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GliderScoreRemoteForm_FormClosing);
            this.Load += new System.EventHandler(this.GliderScoreRemoteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbPollingFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBroadcastDelay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tpStatus.ResumeLayout(false);
            this.tpStatus.PerformLayout();
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbPollingFrequency;
        private System.Windows.Forms.Label lPollingFrequencyValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lGliderScoreTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lBroadcastDelay;
        private System.Windows.Forms.TrackBar tbBroadcastDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbUDPTimerPort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lUDPTimerRecdData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbUDPTelemetryPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lUDPTelemetryRecdData;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpStatus;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.TabControl tcRemoteStatus;
        private System.Windows.Forms.ComboBox cbTimerComPort;
        private System.Windows.Forms.Label label8;
    }
}

