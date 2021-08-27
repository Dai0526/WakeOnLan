using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WakeOnLan
{
    public class Configuration
    {

        public Dictionary<string, ComputerInfo> m_pcInfo;

        public Configuration()
        {

        }

        public bool ReadConfigXml(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //throw Exception("Configuration Path is null or empty.");
                return false;
            }

            XDocument xdoc = XDocument.Load(path);
            IEnumerable<XElement> nodes = xdoc.DescendantNodes().OfType<XElement>();

            int numNodes = nodes.Count<XElement>();

            if (numNodes == 0)
            {
                return false;
            }

            m_pcInfo = new Dictionary<string, ComputerInfo>();

            foreach (XElement xe in nodes)
            {
                string key = xe.Name.ToString();

                if(string.Compare(key, "Node", true) == 0)
                {
                    string id = xe.Attribute("id").Value;
                    string ip = xe.Attribute("ip").Value;
                    string mac = xe.Attribute("mac").Value;
                    string desc = xe.Attribute("description").Value;
                    ComputerInfo info = new ComputerInfo(ip, mac, id, desc);
                    m_pcInfo.Add(id, info);
                }
            }

            return true;
    }

        public void WriteConfigXml(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //throw Exception("Configuration Path is null or empty.");
                return;
            }
        }

    }
}
