using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.Converting
{
    public interface ITransponderDataConversion
    {
        Track ConvertData(string receivedData);
    }
}