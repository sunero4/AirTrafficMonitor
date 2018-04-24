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

        public override string ToString()
        {
            var stringRepresentation = $"Tag: {Tag} \n" +
                                       $"Position: X: {Position.X}, Y: {Position.Y} \n" +
                                       $"Altitude: {Altitude} \n" +
                                       $"Timestamp: {TimeStamp} \n" +
                                       $"Velocity: {Velocity} m/s \n" +
                                       $"Course: {Course} degrees";
            return stringRepresentation;
        }
    }
}
