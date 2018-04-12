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
        public bool IsPlaneInAirspace(IAirspace airspace, Coordinates planeCoordinates, double planeAltitude)
        {
            //Checks if plane is in airspace
            return !(planeCoordinates.X > airspace.NorthEastCorner.X || planeCoordinates.X < airspace.SoutWestCorner.X ||
                planeCoordinates.Y > airspace.NorthEastCorner.Y || planeCoordinates.Y < airspace.SoutWestCorner.Y ||
                planeAltitude > airspace.UpperAltitudeBoundary || planeAltitude < airspace.LowerAltitudeBoundary);
        }
    }
}
