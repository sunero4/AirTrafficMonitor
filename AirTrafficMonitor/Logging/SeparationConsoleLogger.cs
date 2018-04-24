using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;

namespace AirTrafficMonitor.Logging
{
    public class SeparationConsoleLogger : ISeparationConsoleLogger
    {
        public void LogSeparationToConsole(object sender, SeparationEventArgs separationEvent)
        {
            var separationString = $"{separationEvent.Track1} og {separationEvent.Track2} er på kollisionskurs\n" +
                                   $"Timestamp: {separationEvent.TimeOfOccurence} \n" +
                                   "Mayday mayday";
            Console.WriteLine(separationString);
        }
    }
}