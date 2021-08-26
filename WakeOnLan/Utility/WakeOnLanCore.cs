using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WakeOnLan.Utility
{
    public class WakeOnLanCore
    {

        private const UInt16 Port = 9;
        private UdpClient m_client;

        public WakeOnLanCore()
        {

        }

        public void Wake(string mac, string ip = "")
        {

        }

        public void Wake(PhysicalAddress mac, IPAddress ip = null)
        {

        }

        public bool Ping(string ip)
        {
            return true;
        }


        /*
         TODO
         1. Launch a thread to check PC status by ping in 2 mins (slow start methods) 
         */
    }
}
