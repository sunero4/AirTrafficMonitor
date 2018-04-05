using System;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Rendering;

namespace AirTrafficMonitor.Logging
{
    public class TrackConsoleLogging : ITrackLogging
    {
        private readonly ITrackToStringRepresentation _trackToStringRepresentation;

        public TrackConsoleLogging(ITrackToStringRepresentation trackToStringRepresentation)
        {
            _trackToStringRepresentation = trackToStringRepresentation;
        }

        public void LogTrack(Track track)
        {
            var stringRepresentation = _trackToStringRepresentation.GenerateStringRepresentation(track);
            Console.WriteLine(stringRepresentation);
        }
    }
}