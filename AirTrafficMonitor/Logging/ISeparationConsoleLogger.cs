using AirTrafficMonitor.AirspaceManagement;

namespace AirTrafficMonitor.Logging
{
    public interface ISeparationConsoleLogger
    {
        void LogSeparationToConsole(object sender, SeparationEventArgs separationEvent);
    }
}