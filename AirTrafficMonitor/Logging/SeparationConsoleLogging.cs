using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;

namespace AirTrafficMonitor.Logging
{
    public class SeparationConsoleLogging: ISeparationConsoleLogging
    {
        public void LogSeparation(object sender, SeparationEventArgs separationEvent)
        {
            Console.WriteLine($"{separationEvent.Track1} og {separationEvent.Track2} er på kollisionskurs\n" +
                                       $"Timestamp: {separationEvent.TimeOfOccurence} \n" +
                                       "Mayday mayday"
                                        );
        }
    }
}
