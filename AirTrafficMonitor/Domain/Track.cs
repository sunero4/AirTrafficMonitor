using System;

namespace AirTrafficMonitor.Domain
{
    public class Track
    {
        public string Tag { get; set; }
        public Coordinates Position { get; set; }
        public double Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public Double Velocity { get; set; }
        public double Course { get; set; }
    }
}
