using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public interface IAirspaceMonitoring
    {
        bool IsPlaneInAirspace(Coordinates planeCoordinates, double planeAltitude);
    }
}