using System;
using System.Collections.Generic;

namespace AirTrafficMonitor.Domain
{
    public interface IAirspace
    {
        int LowerAltitudeBoundary { get; set; }
        Coordinates NorthEastCorner { get; set; }
        Dictionary<string, List<Track>> PlanesInAirspace { get; set; }
        Coordinates SoutWestCorner { get; set; }
        int UpperAltitudeBoundary { get; set; }
        void OnMovementInAirspaceDetected(object sender, TrackEventArgs trackEventArgs);
    }
}