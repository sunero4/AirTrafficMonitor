using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.AirspaceManagement
{
    public interface IAirspaceMonitoring
    {
        void CheckIfPlaneIsInAirspace(Track track);
        bool IsPlaneInAirspace(Track track);
    }
}