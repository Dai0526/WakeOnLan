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

        public bool WriteConfigXml(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //throw Exception("Configuration Path is null or empty.");
                return false;
            }

            if(m_pcInfo == null || m_pcInfo.Count == 0)
            {
                return true;
            }

            XDocument doc = new XDocument();
            XElement root = new XElement("Computers",
                                m_pcInfo.Values.Select(x => new XElement("Node", new XAttribute[] {
                                    new XAttribute("id", x.id),
                                    new XAttribute("ip", x.GetIPString()),
                                    new XAttribute("mac", x.GetMACString()),
                                    new XAttribute("description", x.description) } )));
            doc.Add(root);
            doc.Save(@path);
            return true;
        }

    }
}
