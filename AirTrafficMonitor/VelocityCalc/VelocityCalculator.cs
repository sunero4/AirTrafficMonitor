using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.VelocityCalc
{
    public class VelocityCalculator: IVelocityCalculator
    {
        public void CalculateVelocity(List<Track> Tracklist)
        {
            double Timedifference = Tracklist[1].TimeStamp.Subtract(Tracklist[0].TimeStamp).TotalSeconds; 
            double AltitudeDifference = Math.Abs(Tracklist[1].Altitude - Tracklist[0].Altitude);
            double PositionDifferenceX = -Math.Abs(Tracklist[1].Position.X - Tracklist[0].Position.X);
            double PositionDifferenceY = -Math.Abs(Tracklist[1].Position.Y - Tracklist[0].Position.Y);
            
            double HorizontalVelocity = AltitudeDifference / Timedifference;
            double VerticalVelocity = (PositionDifferenceX + PositionDifferenceY) / Timedifference;

            Tracklist[1].Velocity = Math.Sqrt(Math.Pow(HorizontalVelocity, 2) + Math.Pow(HorizontalVelocity, 2));
        }
    }
}
