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
        private readonly Airspace _airspace;
        private readonly IAirspaceMovementMonitoring _airspaceMovementMonitoring;
        public event EventHandler<TrackEventArgs> PlaneIsInAirSpace;
        public event EventHandler<TrackEventArgs> PlaneIsNotInAirSpace; 
        public AirspaceMonitoring(Airspace airspace, IAirspaceMovementMonitoring airspaceMovementMonitoring)
        {
            _airspace = airspace;
            _airspaceMovementMonitoring = airspaceMovementMonitoring;
            PlaneIsInAirSpace += _airspaceMovementMonitoring.OnMovementInAirspaceDetected;
            PlaneIsNotInAirSpace += _airspaceMovementMonitoring.OnPlaneNotInAirspace;
        }

        public void CheckIfPlaneIsInAirspace(Track track)
        {
            if (IsPlaneInAirspace(track))
            {
                PlaneIsInAirSpace?.Invoke(this, new TrackEventArgs() {Track = track});
            }
            else
            {
                PlaneIsNotInAirSpace?.Invoke(this, new TrackEventArgs() {Track = track});
            }
        }

        public bool IsPlaneInAirspace(Track track)
        {
            return !(track.Position.X > _airspace.NorthEastCorner.X || track.Position.X < _airspace.SoutWestCorner.X ||
                     track.Position.Y > _airspace.NorthEastCorner.Y || track.Position.Y < _airspace.SoutWestCorner.Y ||
                     track.Altitude > _airspace.UpperAltitudeBoundary || track.Altitude < _airspace.LowerAltitudeBoundary);
        }
    }
}
