using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GliderScoreRemote
{
    public partial class StatusItem : UserControl
    {
        private double dLow;
        private double dHigh;

        public StatusItem()
        {
            InitializeComponent();
        }

        public StatusItem(RemoteStatus rs, String itemName)
        {
            InitializeComponent();

            rs.SuspendLayout();
            rs.Controls.Add(this);
            this.Name = itemName;
            // need to set location based on alphabetical order of item names
            int index = 0;
            foreach (Control c in rs.Controls.Cast<Control>().OrderBy(c => c.Name))
            {
                if (c.GetType().Equals(typeof(StatusItem)))
                {
                    c.Location = new Point(3, 55 + (this.Height * index++));
                }
            }
            rs.ResumeLayout();

            lItemName.Text = itemName;
        }

        public void UpdateValue(String value)
        {
            //System.Diagnostics.Debug.WriteLine("Item: " + this.Name + " Value: " + value);
            double dValue = Convert.ToDouble(value);
            if (this.lLowValue.Text.Equals("LowValue"))
            {
                dLow = dValue;
                dHigh = dValue;
                lLowValue.Text = value;
                lHighValue.Text = value;
                tbValue.Value = 50;

            }
            if (dLow > dValue)
            {
                dLow = dValue; // new low value
                lLowValue.Text = value;
            }
            if (dHigh < dValue)
            {
                dHigh = dValue; // new high value
                lHighValue.Text = value;
            }
            if (dLow == dHigh)
            {
                tbValue.Value = 50; // put it in the middle
            }
            else
            {
                tbValue.Value = (int) ((dValue - dLow) / (dHigh - dLow) * 100.0);
            }
            lCurrentValue.Text = value;
        }
    }
}
