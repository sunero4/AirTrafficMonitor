using System;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.Logging
{
    public class TrackConsoleLogging : ITrackLogging
    {
        public void LogTrack(Track track)
        {
            Console.WriteLine(track.ToString());
        }
    }
}