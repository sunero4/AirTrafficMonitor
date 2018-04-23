using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Logging;

namespace AirTrafficMonitor.Rendering
{
    public class SeparationToStringRepresentation : ISeparationToStringRepresentation
    {
        public string GenerateSeparationString(SeparationEventArgs separationEvent)
        {
            var separationString = $"{separationEvent.Track1} og {separationEvent.Track2} er på kollisionskurs\n" +
                                   $"Timestamp: {separationEvent.TimeOfOccurence} \n" +
                                   "Mayday mayday";
            return separationString;
        }
    }
}