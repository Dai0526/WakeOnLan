using System;
using System.Net;
using System.Net.NetworkInformation;


namespace WakeOnLan
{
    public enum PCStatus
    {
        UNKNOW = 0,
        ONLINE = 1,
        OFFLINE = 2
    }

    public class ComputerInfo
    {
        public IPAddress IP { get; set; } = null;
        public PhysicalAddress macAddress { get; set; } = null;
        public string id { get; set; } = null;
        public string description { get; set; } = null;
        public PCStatus status { get; set; } = PCStatus.UNKNOW;
        public DateTime lastChecked { get; set; }

        public ComputerInfo(string ip, string mac, string name, string desc = "")
        {
            IP = ParseIp(ip);
            macAddress = ParseMacAddress(mac.ToUpper().Replace(':', '-'));
            description = desc;
            id = name;
        }

        private IPAddress ParseIp(string ip)
        {
            IPAddress address = null;

            try
            {
                address = IPAddress.Parse(ip);
            }
            catch (ArgumentNullException ae)
            {
                Console.WriteLine("ArgumentNullException: " + ae.Source + ", msg = " + ae.Message);
            }
            catch (FormatException fe)
            {
                Console.WriteLine("FormatException: " + fe.Source + ", msg = " + fe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Source + ", msg = " + e.Message);
            }

            return address;
        }

        private PhysicalAddress ParseMacAddress(string mac)
        {
            PhysicalAddress address = null;

            try
            {
                address = PhysicalAddress.Parse(mac);
            }
            catch (ArgumentNullException ae)
            {
                Console.WriteLine("ArgumentNullException: " + ae.Source + ", msg = " + ae.Message);
            }
            catch (FormatException fe)
            {
                Console.WriteLine("FormatException: " + fe.Source + ", msg = " + fe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Source + ", msg = " + e.Message);
            }

            return address;
        }
    }
}
