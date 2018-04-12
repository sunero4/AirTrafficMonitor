using System;

namespace AirTrafficMonitor.Domain
{
    public class TrackEventArgs : EventArgs
    {
        public Track Track { get; set; }
    }
}