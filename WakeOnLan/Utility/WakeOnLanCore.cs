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
        private readonly IPAddress braodcastIp = IPAddress.Broadcast;

        private UdpClient m_client = null;

        public WakeOnLanCore()
        {
            m_client = new UdpClient();
        }

        public void Wake(PhysicalAddress mac, IPAddress ip = null)
        {
            if (ip != null && ip != IPAddress.Loopback)
            {
                SendP2P(mac, ip);
            }

            SendBroadcast(mac);
        }

        public void Wake(ComputerInfo info)
        {
            if(info.IP != null && info.IP != IPAddress.Loopback){
                SendP2P(info.macAddress, info.IP);
            }
            // send both methods - sometime ip will change do to DNS changes, thus double check
            SendBroadcast(info.macAddress);
        }

        private bool SendBroadcast(PhysicalAddress mac)
        {
            var magic = MakeMagicPacket(mac);
            m_client.Send(magic, magic.Length, new IPEndPoint(braodcastIp, Port));
            return true;
        }

        private bool SendP2P(PhysicalAddress mac, IPAddress ip)
        {
            var magic = MakeMagicPacket(mac);
            m_client.Send(magic, magic.Length, new IPEndPoint(ip, Port));
            return true;
        }

        private byte[] MakeMagicPacket(PhysicalAddress mac)
        {
            return Enumerable.Repeat(Byte.MaxValue, 6).Concat(Enumerable.Repeat(mac.GetAddressBytes(), 16).SelectMany(x => x)).ToArray();
        }

        public bool PingTarget(ComputerInfo info)
        {
            return PingTarget(info.IP);
        }

        public bool PingTarget(IPAddress ip)
        {
            return PingTarget(ip.ToString());
        }

        public bool PingTarget(string ip)
        {
            try
            {
                Ping pg = new Ping();
                PingReply rp = pg.Send(ip);
                return rp.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }



        /*
         TODO - extra
         1. Launch a thread to check PC status by ping in 2 mins (slow start methods) 
         */
    }
}
