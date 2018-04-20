using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.CourseCalculations
{
    public class DegreesCalculatorWithoutDecimals : IDegreesCalculator
    {
        public double CalculateDegrees(List<Track> tracks)
        {
            var x1 = tracks[0].Position.X;
            var x2 = tracks[1].Position.X;
            var y1 = tracks[0].Position.Y;
            var y2 = tracks[1].Position.Y;

            var radians = Math.Atan2(y2 - y1, x2 - x1);
            var degrees = (Math.Round((radians * (180 / Math.PI)), 0));

            if (degrees < 0)
            {
                degrees = degrees + 360;
            }

            if (degrees > 359)
            {
                degrees = 0;
            }

            return degrees;
        }
    }
}
