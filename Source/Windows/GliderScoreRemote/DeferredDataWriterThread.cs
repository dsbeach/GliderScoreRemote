using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GliderScoreRemote
{
    class DeferredDataWriterThread
    {
        private Thread writerThread;
        private BlockingCollection<DeferredData> queue;
        // list of UdpClients to send multicasts
        private List<UdpClient> sendClients = new List<UdpClient>();
        SerialPort serialPort = new SerialPort();

        public DeferredDataWriterThread(ref BlockingCollection<DeferredData> queue)
        {
            this.queue = queue;

            // join multicast group on all available network interfaces
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if ((!networkInterface.Supports(NetworkInterfaceComponent.IPv4)) ||
                    (networkInterface.OperationalStatus != OperationalStatus.Up))
                {
                    continue;
                }

                IPInterfaceProperties adapterProperties = networkInterface.GetIPProperties();
                UnicastIPAddressInformationCollection unicastIPAddresses = adapterProperties.UnicastAddresses;
                IPAddress ipAddress = null;

                foreach (UnicastIPAddressInformation unicastIPAddress in unicastIPAddresses)
                {
                    if (unicastIPAddress.Address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        continue;
                    }

                    ipAddress = unicastIPAddress.Address;
                    break;
                }

                if (ipAddress == null)
                {
                    continue;
                }

                UdpClient sendClient = new UdpClient(new IPEndPoint(ipAddress,0));
                sendClients.Add(sendClient);
            }

        }

        public void Start()
        {
            writerThread = new Thread(writeDeferredData);
            writerThread.Start();
        }

        public void Stop()
        {
            if (serialPort.IsOpen)
            {
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
                serialPort.Close();
            }
            writerThread.Abort();
            writerThread.Join();
        }

        public bool IsAlive()
        {
            return writerThread.IsAlive;
        }

        public void writeDeferredData()
        {
            IPEndPoint ipEndPointBroadcast;
            try
            {
                DeferredData data;
                while (true)
                {
                    data = queue.Take(); // blocks until data is available

                    // HERE I AM - detect a change in com port parameters and handle accordingly

                    System.Diagnostics.Debug.WriteLine(String.Format("UDPPort: {0}, ComPort: {1}, SendTime: {2}, Message: {3}", data.udpPort, data.comPort, data.sendTime, data.message));

                    int delay = (int)data.sendTime.Subtract(DateTime.Now).TotalMilliseconds;
                    if (delay > 0)
                    {
                        Thread.Sleep(delay);
                    }

                    // handle the UDP broadcast
                    try
                    {
                        ipEndPointBroadcast = new IPEndPoint(IPAddress.Broadcast, data.udpPort);
                        byte[] udpDataBytes = Encoding.ASCII.GetBytes(data.message);
                        foreach (UdpClient udpClient in sendClients)
                        {
                            udpClient.Send(udpDataBytes, udpDataBytes.Length, ipEndPointBroadcast);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("UDP Send error: " + ex.Message);
                    }

                    // handle the serial write
                    try
                    {
                        // ignore if com port is <none>
                        if (!data.comPort.Equals("<none>"))
                        {
                            if (!serialPort.PortName.Equals(data.comPort)) // switch ports
                            {
                                // close the old one if open
                                if (serialPort.IsOpen) {
                                    serialPort.DiscardInBuffer();
                                    serialPort.DiscardOutBuffer();
                                    serialPort.Close();
                                }
                                // set the parameters
                                serialPort.BaudRate = 9600;
                                serialPort.PortName = data.comPort;
                                serialPort.DataBits = 8;
                                serialPort.Parity = Parity.None;
                                serialPort.StopBits = StopBits.One;
                                serialPort.Handshake = Handshake.None;
                                serialPort.DtrEnable = true;
                                serialPort.WriteTimeout = 250;
                                serialPort.Open();
                            }
                            // if the port is open but nobody is taking the data, just flush it!
                            serialPort.DiscardOutBuffer();
                            serialPort.Write(data.message);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Serial send error: " + ex.Message);
                    }


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("writeDeferredData thread exiting - Exception: " + ex.Message);
            }
        }
    }
}
