using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor.Logging
{
    public class SeparationConsoleLogging: ISeparationConsoleLogging
    {
        public void LogSeparation(string Track1, string Track2, DateTime TimeOfOccurence)
        {
            Console.WriteLine($"{Track1} og { Track2} er på kollisionskurs\n" +
                                       $"Timestamp: {TimeOfOccurence} \n" +
                                       "Mayday mayday"
                                        );
        }
    }
}
