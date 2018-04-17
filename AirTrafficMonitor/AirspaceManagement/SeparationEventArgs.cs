using System;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class SeparationEventArgs : EventArgs
    {
        public string Track1;
        public string Track2;
        public DateTime TimeOfOccurence;
    }
}