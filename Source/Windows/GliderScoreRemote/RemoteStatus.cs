using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace GliderScoreRemote
{
    public partial class RemoteStatus : UserControl
    {
        private TabPage tp;
        List<StatusItem> statusItems = new List<StatusItem>();
        DateTime dtLastUpdate;
        System.Timers.Timer tUpdateTimer;

        public RemoteStatus()
        {
            InitializeComponent();
        }

        private void HandleTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (lUpdateAge.InvokeRequired)
            {
                lUpdateAge.Invoke(new Action<Object, System.Timers.ElapsedEventArgs>(HandleTimer), source, e);
                return;
            }
            lUpdateAge.Text = ((int)(DateTime.Now.Subtract(dtLastUpdate).TotalSeconds)).ToString();
            //System.Diagnostics.Debug.WriteLine("HandleTimer(): ");
        }

        public RemoteStatus(TabPage tp)
        {
            InitializeComponent();

            this.tp = tp;
            tp.SuspendLayout();
            this.Size = tp.ClientSize;
            tp.Controls.Add(this);
            tp.ResumeLayout();

            tUpdateTimer = new System.Timers.Timer(1000.0);
            tUpdateTimer.Elapsed += HandleTimer;
            tUpdateTimer.AutoReset = true;
            tUpdateTimer.Enabled = true;

            lIP1.Text = "";
            lIP2.Text = "";
        }

        public void UpdateStatus(String sStatus)
        {
            List<Tuple<String, String>> items = new List<Tuple<string, string>>();

            //System.Diagnostics.Debug.WriteLine("UpdateStatus: " + tp.Text + ", " + sStatus);
            // parsing logic - messages in the form "token=value;token=value;..."
            string[] tokens = sStatus.Split(';');
            foreach (string token in tokens)
            {
                string[] values = token.Split('=');
                switch (values[0])
                {
                    case "hostname":
                        break;
                    case "ips":
                        string[] ips = values[1].Split(',');
                        if (ips.Length >= 1)
                        {
                            lIP1.Text = ips[0];
                        }
                        if (ips.Length >=2)
                        {
                            lIP2.Text = ips[1].Trim();
                        }
                        break;
                    default:
                        items.Add(new Tuple<string, string>(values[0], values[1]));
                        break;
                }
            }
            foreach (Tuple<String, String> t in items)
            {
                //System.Diagnostics.Debug.WriteLine("Update-> category:" + t.Item1 + " value:" + t.Item2);
                StatusItem currentItem = null;
                foreach (StatusItem si in statusItems)
                {
                    if (si.Name.Equals(t.Item1))
                    {
                        currentItem = si;
                    }
                }
                if (currentItem == null)
                {
                    currentItem = new StatusItem(this, t.Item1);
                    statusItems.Add(currentItem);
                }
                currentItem.UpdateValue(t.Item2);
            }
            dtLastUpdate = DateTime.Now;
        }

        private void IP_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + ((Label)sender).Text);
        }
    }
}
