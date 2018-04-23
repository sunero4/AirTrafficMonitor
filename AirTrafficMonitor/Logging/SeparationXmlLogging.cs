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
        private XmlFileLogging _xmlFileLogging;

        public SeparationXmlLogging()
        {
            var _location = Directory.GetCurrentDirectory();
        }

        public void LogSeparation(object sender, SeparationEventArgs separationEvent)
        {
            var xmlDocument = XDocument.Load(_location + "SeparationLog.xml");
            var root = xmlDocument.Root;
            root.Add(_xmlFileLogging.WriteXmlFile(separationEvent.Track1, separationEvent.Track2,
                separationEvent.TimeOfOccurence));
            xmlDocument.Save(_location + "SeparationLog.xml");
        }
    }
}
