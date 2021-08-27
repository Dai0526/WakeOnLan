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
        private UdpClient m_client = null;

        public WakeOnLanCore()
        {

        }

        public void Wake(string mac, string ip = "")
        {
            // TODO
        }

        public void Wake(PhysicalAddress mac, IPAddress ip = null)
        {
            // TODO
        }

        public void Wake(ComputerInfo info)
        {
            // TODO
        }

        public bool Ping(string ip)
        {
            return true;
        }


        /*
         TODO - extra
         1. Launch a thread to check PC status by ping in 2 mins (slow start methods) 
         */
    }
}
