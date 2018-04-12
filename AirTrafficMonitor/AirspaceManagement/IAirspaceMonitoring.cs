using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public interface IAirspaceMonitoring
    {
        bool IsPlaneInAirspace(IAirspace airspace, Coordinates planeCoordinates, double planeAltitude);
    }
}