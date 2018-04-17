using System;
using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public interface ISeparation
    {
        event EventHandler<SeparationEventArgs> SeparationEvent;

        void MonitorSeparation(Dictionary<string, List<Track>> tracks);
    }
}