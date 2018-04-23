using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.CourseCalculations
{
    public interface IDegreesCalculator
    {
        void CalculateDegrees(List<Track> tracks);
    }
}