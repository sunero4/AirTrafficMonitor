using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.AirspaceManagement;
using System;

namespace AirTrafficMonitor.Logging
{
    public interface ISeparationXmlLogging
    {
        void LogSeparation(object sender, SeparationEventArgs separationEvent);
    }
}