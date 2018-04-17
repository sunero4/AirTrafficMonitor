using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public interface IAirspace
    {
        int LowerAltitudeBoundary { get; set; }
        Coordinates NorthEastCorner { get; set; }
        Dictionary<string, List<Track>> PlanesInAirspace { get; set; }
        Coordinates SoutWestCorner { get; set; }
        int UpperAltitudeBoundary { get; set; }
    }
}