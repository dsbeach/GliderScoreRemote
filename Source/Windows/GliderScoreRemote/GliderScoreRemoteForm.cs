using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GliderScoreRemote
{
    public partial class GliderScoreRemoteForm : Form
    {
        private BlockingCollection<DeferredData> queue;
        private UDPReaderThread timerThread = new UDPReaderThread();
        private UDPReaderThread telemetryThread = new UDPReaderThread();
        private DeferredDataWriterThread writerThread;
        private bool blockRecursion = false;
        private int trackValue;

        public GliderScoreRemoteForm()
        {
            InitializeComponent();

            tcMain.DrawItem += TabPage_DrawItem;
            tcRemoteStatus.DrawItem += TabPage_DrawItem;
            NetworkChange.NetworkAddressChanged += new
            NetworkAddressChangedEventHandler(NetworkAddressChangedCallback);
        }

        private void GliderScoreRemoteForm_Load(object sender, EventArgs e)
        {
            tbPollingFrequency.Value = Properties.Settings.Default.PollingFrequency;
            tbBroadcastDelay.Value = Properties.Settings.Default.BroadcastDelay;
            tbUDPTimerPort.Text = Properties.Settings.Default.UDPTimerPort.ToString();
            tbUDPTelemetryPort.Text = Properties.Settings.Default.UDPTelemetryPort.ToString();

            // initialize the queue
            queue = new BlockingCollection<DeferredData>();
            writerThread = new DeferredDataWriterThread(ref queue);

            // thread start order is important
            timerThread.Start(Properties.Settings.Default.UDPTimerPort, UDPTimerReadCallback);
            telemetryThread.Start(Properties.Settings.Default.UDPTelemetryPort, UDPTelemetryReadCallback);
            writerThread.Start();

            GliderScoreWindowScraperThread.WaitInterval = 1000 / tbPollingFrequency.Value;
            GliderScoreWindowScraperThread.Start(TimeCallback, LBSelectedTextCallback);

            string selectedTimerComPort = Properties.Settings.Default.TimerComPort;
            cbTimerComPort_DropDown(null, null); // load up the list of current com ports
            if (cbTimerComPort.Items.Contains(selectedTimerComPort))
            {
                cbTimerComPort.SelectedItem = selectedTimerComPort;
            }
            else
            {
                cbTimerComPort.SelectedItem = "<none>";
            }
            cbTimerComPort.DropDown += cbTimerComPort_DropDown; // this will refresh the list when 'dropped'
        }

        private void GliderScoreRemoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GliderScoreWindowScraperThread.Stop();
            writerThread.Stop();
            telemetryThread.Stop();
            timerThread.Stop();
        }

        private void tbPollingFrequency_ValueChanged(object sender, EventArgs e)
        {
            GliderScoreWindowScraperThread.WaitInterval = 1000 / tbPollingFrequency.Value;
            lPollingFrequencyValue.Text = tbPollingFrequency.Value.ToString();
            Properties.Settings.Default.PollingFrequency = tbPollingFrequency.Value;
            Properties.Settings.Default.Save();
        }

        public void TimeCallback(string sTime)
        {
            if (lGliderScoreTime.InvokeRequired)
            {
                lGliderScoreTime.Invoke(new Action<string>(TimeCallback), sTime);
                return;
            }
            //Debug.WriteLine("TimeCallback(): " + sTime);
            lGliderScoreTime.Text = sTime;
            if (!sTime.Equals("n/a"))
            {
                string panelString = sTime.Replace("Time=", "");
                panelString = "A" + panelString.Replace(":", "") + " \r";

                // any changes to parameters are persisted in settings already and passed on in the deferred data
                DeferredData data = new DeferredData();
                data.udpPort = Properties.Settings.Default.UDPTimerPort;
                data.comPort = Properties.Settings.Default.TimerComPort;
                data.sendTime = DateTime.Now.AddMilliseconds(Properties.Settings.Default.BroadcastDelay);
                data.message = panelString;
                queue.Add(data);
            }
            else
            {
                lUDPTimerRecdData.Text = "n/a";
            }
        }

        public void UDPTimerReadCallback(string sTime)
        {
            if (lUDPTimerRecdData.InvokeRequired)
            {
                lUDPTimerRecdData.Invoke(new Action<string>(UDPTimerReadCallback), sTime);
                return;
            }
            string formattedTime = sTime.Substring(1, 2) + ":" + sTime.Substring(3, 2);
            //Debug.WriteLine("UDPTimerReadCallback(): " + formattedTime);
            lUDPTimerRecdData.Text = formattedTime;
        }

        public void UDPTelemetryReadCallback(string sTelemetry)
        {
            if (lUDPTelemetryRecdData.InvokeRequired)
            {
                lUDPTelemetryRecdData.Invoke(new Action<string>(UDPTelemetryReadCallback), sTelemetry);
                return;
            }
            //Debug.WriteLine("UDPTelemetryReadCallback(): " + sTelemetry);
            lUDPTelemetryRecdData.Text = sTelemetry;

            String hostname = null;

            // parsing logic - messages in the form "token=value;token=value;..."
            string[] tokens = sTelemetry.Split(';');
            foreach (string token in tokens)
            {
                string[] values = token.Split('=');
                switch (values[0])
                {
                    case "hostname":
                        hostname = values[1];
                        break;
                }
            }
            // find the tab associated with this server
            TabPage tpRemoteStatus = null;
            foreach (TabPage tp in tcRemoteStatus.TabPages)
            {
                if (tp.Text.Equals(hostname))
                {
                    tpRemoteStatus = tp;
                }
            }
            if (tpRemoteStatus == null)
            {
                tpRemoteStatus = new TabPage(hostname);
                tpRemoteStatus.Size = tcRemoteStatus.ClientSize;
                tpRemoteStatus.Tag = new RemoteStatus(tpRemoteStatus);
                tpRemoteStatus.BackColor = SystemColors.AppWorkspace;
                //tpRemoteStatus.ForeColor = SystemColors.Control;
                // need to keep the tab pages in alphabetical order
                int index = 0;
                foreach (TabPage tp in tcRemoteStatus.TabPages.Cast<TabPage>().OrderBy(c => c.Text))
                {
                    if (tp.Text.CompareTo(hostname) == 1)
                    {
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
                tcRemoteStatus.TabPages.Insert(index, tpRemoteStatus);
                //tcRemoteStatus.TabPages.Add(tpRemoteStatus);
            }
           ((RemoteStatus)tpRemoteStatus.Tag).UpdateStatus(sTelemetry);
        }

        public void LBSelectedTextCallback(string lbSelectedText)
        {
            //Debug.WriteLine("LBSelectedTextCallback(): " + lbSelectedText);
        }

        private void tbBroadcastDelay_ValueChanged(object sender, EventArgs e)
        {
            if (blockRecursion) return;

            trackValue = ((TrackBar)sender).Value;

            if (trackValue % ((TrackBar)sender).SmallChange != 0)
            {
                trackValue = (trackValue / ((TrackBar)sender).SmallChange * ((TrackBar)sender).SmallChange);

                // see note #1 below
                blockRecursion = true;

                ((TrackBar)sender).Value = trackValue;

                blockRecursion = false;
            }
            lBroadcastDelay.Text = tbBroadcastDelay.Value.ToString();
            Properties.Settings.Default.BroadcastDelay = tbBroadcastDelay.Value;
            Properties.Settings.Default.Save();
        }

        private void tbUDPPort_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(tbUDPTimerPort.Text);
                Properties.Settings.Default.UDPTimerPort = result;
                timerThread.ChangePort(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("UDP Timer Port must be numeric! Exception: " + ex.Message, "Correct and retry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbUDPTelemetryPort_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(tbUDPTelemetryPort.Text);
                Properties.Settings.Default.UDPTelemetryPort = result;
                telemetryThread.ChangePort(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("UDP Telemetry Port must be numeric! Exception: " + ex.Message, "Correct and retry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void NetworkAddressChangedCallback(object sender, EventArgs e)
        {
            tbUDPTelemetryPort_TextChanged(null, null); // force listening on all available interfaces
            writerThread.Stop();
            writerThread.Start(); // will reset adapter send list
        }

        private void TabPage_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                TabPage page = ((TabControl)sender).TabPages[e.Index];
                SolidBrush bkgrndBrush;
                Color textColor;
                int yOffset;
                if (e.State == DrawItemState.Selected)
                {
                    bkgrndBrush = new SolidBrush(page.BackColor);
                    textColor = page.ForeColor;
                    yOffset = -2;
                }
                else
                {
                    bkgrndBrush = new SolidBrush(SystemColors.ControlLight);
                    textColor = SystemColors.ControlDark;
                    yOffset = 1;
                }
                e.Graphics.FillRectangle(bkgrndBrush, e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Offset(1, yOffset);
                TextRenderer.DrawText(e.Graphics, page.Text, Font, paddedBounds, textColor);
            }
            catch { } // possible to get data before the tab page is ready - just ignore it
        }

        private void cbTimerComPort_SelectedValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TimerComPort = cbTimerComPort.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }


        private void cbTimerComPort_DropDown(object sender, System.EventArgs e)
        {
            cbTimerComPort.Items.Clear();
            cbTimerComPort.Items.Add("<none>");
            foreach (string portName in SerialPort.GetPortNames())
            {
                cbTimerComPort.Items.Add(portName);
            }
        }
    }
}
