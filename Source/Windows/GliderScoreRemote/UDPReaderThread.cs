using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GliderScoreRemote
{
    class UDPReaderThread
    {
        private  Thread readerThread;
        private  UdpClient udpClient;
        public delegate void UDPReadCallback(string sTime);
        public  UDPReadCallback udpReadCallback;

        public  void Start(int udpPort, UDPReadCallback udpReadCallback)
        {
            IPAddress multicastAddress = IPAddress.Parse("239.192.1.12"); // why that address?  because it was in the sample code and it works!
            udpClient = new UdpClient(udpPort);

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

                udpClient.JoinMulticastGroup(multicastAddress, ipAddress);

            }

            this.udpReadCallback = udpReadCallback;
            readerThread = new Thread(UdpRead);
            readerThread.Start();
        }

        public  void Stop()
        {
            udpClient.Close();
            readerThread.Abort();
            readerThread.Join();
        }

        public  void ChangePort(int udpPort)
        {
            Stop();
            udpClient = new UdpClient(udpPort);
            readerThread = new Thread(UdpRead);
            readerThread.Start();
        }

        public  void UdpRead()
        {
            try
            {
                while (true)
                {
                    IPEndPoint ipEndPointSender = new IPEndPoint(IPAddress.Any, 0);
                    byte[] result = udpClient.Receive(ref ipEndPointSender);
                    string s = Encoding.ASCII.GetString(result);
                    udpReadCallback(s);
                    //System.Diagnostics.Debug.WriteLine("UdpRead received: " + s);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UdpRead() exiting - Exception: " + ex.Message);
            }
        }
    }
}
