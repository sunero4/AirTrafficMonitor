using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Extensions;
using AirTrafficMonitor.VelocityCalc;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class Airspace : IAirspace
    {
        private readonly IVelocityCalculator _velocityCalculator;

        public Airspace(IVelocityCalculator velocityCalculator, Coordinates southWestCorner, Coordinates northEastCorner, int lowerAltitudeBoundary, int upperAltitudeBoundary)
        {
            _velocityCalculator = velocityCalculator;
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

        public void OnMovementInAirspaceDetected(object sender, TrackEventArgs trackEventArgs)
        {
            if (!PlanesInAirspace.ContainsKey(trackEventArgs.Track.Tag))
            {
                PlanesInAirspace.Add(trackEventArgs.Track.Tag, new List<Track>() {trackEventArgs.Track});
            }
            else
            {
                var planeMovementDetected = PlanesInAirspace.First(x => x.Key == trackEventArgs.Track.Tag).Value;
                planeMovementDetected.AddToSlidingWindowList(trackEventArgs.Track, 2);
                _velocityCalculator.CalculateVelocity(planeMovementDetected);
            }
        }
    }
}
