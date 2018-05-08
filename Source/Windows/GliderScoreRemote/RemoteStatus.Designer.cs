namespace GliderScoreRemote
{
    partial class RemoteStatus
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
            this.label1 = new System.Windows.Forms.Label();
            this.lIP1 = new System.Windows.Forms.Label();
            this.lable3 = new System.Windows.Forms.Label();
            this.lUpdateAge = new System.Windows.Forms.Label();
            this.lIP2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Addresses:";
            // 
            // lIP1
            // 
            this.lIP1.AutoSize = true;
            this.lIP1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lIP1.Location = new System.Drawing.Point(89, 9);
            this.lIP1.Name = "lIP1";
            this.lIP1.Size = new System.Drawing.Size(78, 13);
            this.lIP1.TabIndex = 1;
            this.lIP1.Text = "<ip addresses>";
            this.lIP1.Click += new System.EventHandler(this.IP_Click);
            // 
            // lable3
            // 
            this.lable3.AutoSize = true;
            this.lable3.Location = new System.Drawing.Point(257, 9);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(66, 13);
            this.lable3.TabIndex = 2;
            this.lable3.Text = "Last update:";
            // 
            // lUpdateAge
            // 
            this.lUpdateAge.AutoSize = true;
            this.lUpdateAge.Location = new System.Drawing.Point(329, 9);
            this.lUpdateAge.Name = "lUpdateAge";
            this.lUpdateAge.Size = new System.Drawing.Size(73, 13);
            this.lUpdateAge.TabIndex = 3;
            this.lUpdateAge.Text = "<update age>";
            // 
            // lIP2
            // 
            this.lIP2.AutoSize = true;
            this.lIP2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lIP2.Location = new System.Drawing.Point(173, 9);
            this.lIP2.Name = "lIP2";
            this.lIP2.Size = new System.Drawing.Size(78, 13);
            this.lIP2.TabIndex = 4;
            this.lIP2.Text = "<ip addresses>";
            this.lIP2.Click += new System.EventHandler(this.IP_Click);
            // 
            // RemoteStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.lIP2);
            this.Controls.Add(this.lUpdateAge);
            this.Controls.Add(this.lable3);
            this.Controls.Add(this.lIP1);
            this.Controls.Add(this.label1);
            this.Name = "RemoteStatus";
            this.Size = new System.Drawing.Size(649, 306);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lIP1;
        private System.Windows.Forms.Label lable3;
        private System.Windows.Forms.Label lUpdateAge;
        private System.Windows.Forms.Label lIP2;
    }
}
