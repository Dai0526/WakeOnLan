using System;
using System.Linq;
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
        public DateTime lastChecked { get; set; } = DateTime.MinValue;

        public ComputerInfo(string ip, string mac, string name, string desc = "")
        {
            IP = ParseIp(ip);
            macAddress = ParseMacAddress(mac.ToUpper().Replace(':', '-'));
            description = desc;
            id = name;
        }

        public string GetIPString()
        {
            return IP.ToString();
        }

        public string GetMACString()
        {
            return string.Join("-", macAddress.GetAddressBytes().Select(b => b.ToString("X2")));
        }

        public void UpdateLastCheck()
        {
            lastChecked = DateTime.Now;
        }

        public string GetLastCheckString()
        {
            if(lastChecked.CompareTo(DateTime.MinValue) == 0 || lastChecked == null)
            {
                return "NA";
            }

            return lastChecked.ToString();
        }

        public string GetStatusString()
        {
            return status.ToString();
        }

        public IPAddress ParseIp(string ip)
        {
            IPAddress address = null;

            try
            {
                address = IPAddress.Parse(ip);
            }
            catch (ArgumentNullException ae)
            {
                Console.WriteLine("ArgumentNullException: " + ae.Source + ", msg = " + ae.Message);
                return IP;
            }
            catch (FormatException fe)
            {
                Console.WriteLine("FormatException: " + fe.Source + ", msg = " + fe.Message);
                return IP;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Source + ", msg = " + e.Message);
                return IP;
            }

            return address;
        }

        public PhysicalAddress ParseMacAddress(string mac)
        {
            PhysicalAddress address = null;

            try
            {
                address = PhysicalAddress.Parse(mac);
            }
            catch (ArgumentNullException ae)
            {
                Console.WriteLine("ArgumentNullException: " + ae.Source + ", msg = " + ae.Message);
                return macAddress;
            }
            catch (FormatException fe)
            {
                Console.WriteLine("FormatException: " + fe.Source + ", msg = " + fe.Message);
                return macAddress;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Source + ", msg = " + e.Message);
                return macAddress;
            }

            return address;
        }
    }
}
