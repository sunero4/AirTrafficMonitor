using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitor.CourseCalculations;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Extensions;
using AirTrafficMonitor.Logging;
using AirTrafficMonitor.VelocityCalc;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class AirspaceMovementMonitoring : IAirspaceMovementMonitoring
    {
        private readonly IAirspace _airspace;
        private readonly IVelocityCalculator _velocityCalculator;
        private readonly IDegreesCalculator _degreesCalculator;
        private readonly ITrackLogging _trackLogging;

        public AirspaceMovementMonitoring(IAirspace airspace, IVelocityCalculator velocityCalculator, IDegreesCalculator degreesCalculator, ITrackLogging trackLogging)
        {
            _airspace = airspace;
            _velocityCalculator = velocityCalculator;
            _degreesCalculator = degreesCalculator;
            _trackLogging = trackLogging;
        }
        public void OnMovementInAirspaceDetected(object sender, TrackEventArgs trackEventArgs)
        {
            if (!_airspace.PlanesInAirspace.ContainsKey(trackEventArgs.Track.Tag))
            {
                _airspace.PlanesInAirspace.Add(trackEventArgs.Track.Tag, new List<Track>() { trackEventArgs.Track });
            }
            else
            {
                var planeMovementDetected = _airspace.PlanesInAirspace[trackEventArgs.Track.Tag];
                planeMovementDetected.AddToSlidingWindowList(trackEventArgs.Track, 2);
                _velocityCalculator.CalculateVelocity(planeMovementDetected);
                _degreesCalculator.CalculateDegrees(planeMovementDetected);
            }
            _trackLogging.LogTrack(trackEventArgs.Track);
        }

        public void OnPlaneNotInAirspace(object sender, TrackEventArgs trackEventArgs)
        {
            if (_airspace.PlanesInAirspace.ContainsKey(trackEventArgs.Track.Tag))
            {
                _airspace.PlanesInAirspace.Remove(trackEventArgs.Track.Tag);
            }
        }
    }
}