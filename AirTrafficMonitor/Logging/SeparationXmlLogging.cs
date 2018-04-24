using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Converting;

namespace AirTrafficMonitor.Logging
{
    public class SeparationXmlLogging: ISeparationXmlLogging
    {
        private string _location;

        public SeparationXmlLogging()
        {
            _location = Directory.GetCurrentDirectory();
        }

        public void LogSeparation(object sender, SeparationEventArgs separationEvent)
        {
            var xmlDocument = XDocument.Load(_location + "SeparationLog.xml");
            var root = xmlDocument.Root;
            root.Add(ConvertToXmlElement(separationEvent.Track1, separationEvent.Track2,
                separationEvent.TimeOfOccurence));
            xmlDocument.Save(_location + "SeparationLog.xml");
        }

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
