using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Extensions;

namespace AirTrafficMonitor.Domain
{
    public class Airspace : IAirspace
    {
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
                PlanesInAirspace.First(x => x.Key == trackEventArgs.Track.Tag).Value.AddToSlidingWindowList(trackEventArgs.Track, 2);
                //Her kan den regne hastigheden, og kun her fordi den kun har et punkt i listen hvis den lige er
                //kommet ind i airspace
            }
        }

        public void OnPlaneLeavesAirSpace(object sender, TrackEventArgs trackEventArgs)
        {
            
        }
    }
}
