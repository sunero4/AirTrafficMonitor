using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class AirspaceMonitoring : IAirspaceMonitoring
    {
        private IAirspace _airspace;

        public AirspaceMonitoring(IAirspace airspace)
        {
            _airspace = airspace;
        }

        public bool IsPlaneInAirspace(Coordinates planeCoordinates, double planeAltitude)
        {
            //Checks if plane is in airspace
            return !(planeCoordinates.X > _airspace.NorthEastCorner.X || planeCoordinates.X < _airspace.SoutWestCorner.X ||
                planeCoordinates.Y > _airspace.NorthEastCorner.Y || planeCoordinates.Y < _airspace.SoutWestCorner.Y ||
                planeAltitude > _airspace.UpperAltitudeBoundary || planeAltitude < _airspace.LowerAltitudeBoundary);
        }
    }
}
