using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AirTrafficMonitor.AirspaceManagement;


namespace AirTrafficMonitor.Converting
{
    public class XmlConverting
    {
        public XElement ConvertToXmlElement(string tag1, string tag2, DateTime timeOfOccurence)
        {
            var element = new XElement("SeparationEvent", 
                new XElement("SeparationTag1", tag1), 
                new XElement("SeparationTag2", tag2),
                new XElement("TimeOfSeparationDetected", timeOfOccurence));
            
            return element;
        }
    }
}
