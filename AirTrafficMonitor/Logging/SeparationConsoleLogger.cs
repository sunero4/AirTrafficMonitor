using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Rendering;

namespace AirTrafficMonitor.Logging
{
    public class SeparationConsoleLogger : ISeparationConsoleLogger
    {
        private readonly ISeparationToStringRepresentation _separationToStringRepresentation;

        public SeparationConsoleLogger(ISeparationToStringRepresentation separationToStringRepresentation)
        {
            _separationToStringRepresentation = separationToStringRepresentation;
        }

        public void LogSeparationToConsole(object sender, SeparationEventArgs separationEvent)
        {
            Console.WriteLine(_separationToStringRepresentation.GenerateSeparationString(separationEvent));
        }
    }
}