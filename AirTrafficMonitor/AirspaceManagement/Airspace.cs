using System.Collections.Generic;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.VelocityCalc;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class Airspace : IAirspace
    {

        public Airspace(Coordinates southWestCorner, Coordinates northEastCorner, int lowerAltitudeBoundary, int upperAltitudeBoundary)
        {
            PlanesInAirspace = new Dictionary<string, List<Track>>();
            SoutWestCorner = southWestCorner;
            NorthEastCorner = northEastCorner;
            LowerAltitudeBoundary = lowerAltitudeBoundary;
            UpperAltitudeBoundary = upperAltitudeBoundary;
        }

        public Coordinates SoutWestCorner { get; set; }
        public Coordinates NorthEastCorner { get; set; }
        public int LowerAltitudeBoundary { get; set; }
        public int UpperAltitudeBoundary { get; set; }
        public Dictionary<string, List<Track>> PlanesInAirspace { get; set; }
    }
}
