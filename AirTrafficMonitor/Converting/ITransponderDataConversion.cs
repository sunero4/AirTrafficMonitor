using System.Collections.Generic;
using AirTrafficMonitor.Domain;

namespace AirTrafficMonitor.Converting
{
    public interface ITransponderDataConversion
    {
        void ConvertData(string receivedData);
    }
}