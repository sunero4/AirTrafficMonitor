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
    public class XmlFileLogging
    {

        public XElement WriteXmlFile(string tag1, string tag2, DateTime timeOfOccurence)
        {
            var element = new XElement("SeparationTag1", tag1, "SeparationTag2", tag2, "TimeOfSeparationDetected", timeOfOccurence);
            return element;
        }
    }
}
