using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.VelocityCalc
{
    public interface IVelocityCalculator
    {
        void CalculateVelocity(List<Track> Tracklist);
    }
}