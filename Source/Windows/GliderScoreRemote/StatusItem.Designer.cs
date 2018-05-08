namespace GliderScoreRemote
{
    partial class StatusItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbValue = new System.Windows.Forms.TrackBar();
            this.lItemName = new System.Windows.Forms.Label();
            this.lLowValue = new System.Windows.Forms.Label();
            this.lHighValue = new System.Windows.Forms.Label();
            this.lCurrentValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbValue)).BeginInit();
            this.SuspendLayout();
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(312, 0);
            this.tbValue.Margin = new System.Windows.Forms.Padding(6);
            this.tbValue.Maximum = 100;
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(354, 45);
            this.tbValue.TabIndex = 0;
            this.tbValue.TickFrequency = 10;
            // 
            // lItemName
            // 
            this.lItemName.AutoSize = true;
            this.lItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lItemName.Location = new System.Drawing.Point(33, 0);
            this.lItemName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lItemName.Name = "lItemName";
            this.lItemName.Size = new System.Drawing.Size(101, 24);
            this.lItemName.TabIndex = 1;
            this.lItemName.Text = "Item Name";
            // 
            // lLowValue
            // 
            this.lLowValue.AutoSize = true;
            this.lLowValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLowValue.Location = new System.Drawing.Point(309, 11);
            this.lLowValue.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lLowValue.Name = "lLowValue";
            this.lLowValue.Size = new System.Drawing.Size(54, 13);
            this.lLowValue.TabIndex = 2;
            this.lLowValue.Text = "LowValue";
            this.lLowValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lHighValue
            // 
            this.lHighValue.AutoSize = true;
            this.lHighValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHighValue.Location = new System.Drawing.Point(662, 11);
            this.lHighValue.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lHighValue.Name = "lHighValue";
            this.lHighValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lHighValue.Size = new System.Drawing.Size(56, 13);
            this.lHighValue.TabIndex = 3;
            this.lHighValue.Text = "HighValue";
            this.lHighValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lCurrentValue
            // 
            this.lCurrentValue.AutoSize = true;
            this.lCurrentValue.BackColor = System.Drawing.Color.Transparent;
            this.lCurrentValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCurrentValue.Location = new System.Drawing.Point(232, 0);
            this.lCurrentValue.Name = "lCurrentValue";
            this.lCurrentValue.Size = new System.Drawing.Size(15, 24);
            this.lCurrentValue.TabIndex = 4;
            this.lCurrentValue.Text = ".";
            this.lCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lCurrentValue);
            this.Controls.Add(this.lHighValue);
            this.Controls.Add(this.lLowValue);
            this.Controls.Add(this.lItemName);
            this.Controls.Add(this.tbValue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "StatusItem";
            this.Size = new System.Drawing.Size(1274, 37);
            ((System.ComponentModel.ISupportInitialize)(this.tbValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbValue;
        private System.Windows.Forms.Label lItemName;
        private System.Windows.Forms.Label lLowValue;
        private System.Windows.Forms.Label lHighValue;
        private System.Windows.Forms.Label lCurrentValue;
    }
}
