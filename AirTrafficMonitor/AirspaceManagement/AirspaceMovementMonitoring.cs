using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Extensions;
using AirTrafficMonitor.VelocityCalc;

namespace AirTrafficMonitor.AirspaceManagement
{
    public class AirspaceMovementMonitoring : IAirspaceMovementMonitoring
    {
        private readonly IAirspace _airspace;
        private readonly IVelocityCalculator _velocityCalculator;
        private readonly ISeparation _separation;

        public AirspaceMovementMonitoring(IAirspace airspace, IVelocityCalculator velocityCalculator, ISeparation separation)
        {
            _airspace = airspace;
            _velocityCalculator = velocityCalculator;
            _separation = separation;
        }

        public void OnMovementInAirspaceDetected(object sender, TrackEventArgs trackEventArgs)
        {
            if (!_airspace.PlanesInAirspace.ContainsKey(trackEventArgs.Track.Tag))
            {
                _airspace.PlanesInAirspace.Add(trackEventArgs.Track.Tag, new List<Track>() { trackEventArgs.Track });
            }
            else
            {
                var planeMovementDetected = _airspace.PlanesInAirspace.First(x => x.Key == trackEventArgs.Track.Tag).Value;
                planeMovementDetected.AddToSlidingWindowList(trackEventArgs.Track, 2);
                _velocityCalculator.CalculateVelocity(planeMovementDetected);
            }
        }

        public void CheckSeparation()
        {
            _separation.MonitorSeparation(_airspace.PlanesInAirspace);
        }
    }
}