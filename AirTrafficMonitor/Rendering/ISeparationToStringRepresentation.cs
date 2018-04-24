using AirTrafficMonitor.AirspaceManagement;

namespace AirTrafficMonitor.Rendering
{
    public interface ISeparationToStringRepresentation
    {
        string GenerateSeparationString(SeparationEventArgs separationEvent);
    }
}