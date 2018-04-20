using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.CourseCalculations
{
    public interface IDegreesCalculator
    {
        double CalculateDegrees(List<Track> tracks);
    }
}