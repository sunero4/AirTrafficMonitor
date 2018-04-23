using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.Rendering
{
    public class TrackToStringRepresentation : ITrackToStringRepresentation
    {
        public string GenerateStringRepresentation(Track track)
        {
            var stringRepresentation = $"Tag: {track.Tag} \n" +
                                       $"Position: X: {track.Position.X}, Y: {track.Position.Y} \n" +
                                       $"Altitude: {track.Altitude} \n" +
                                       $"Timestamp: {track.TimeStamp} \n" +
                                       $"Velocity: {track.Velocity} m/s \n" +
                                       $"Course: {track.Course} degrees";
            return stringRepresentation;
        }
    }
}