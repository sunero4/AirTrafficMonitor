using System;

namespace AirTrafficMonitor.Converting
{
    public interface IStringToDateTimeConversion
    {
        DateTime ConvertToDateTime(string dateTimeAsString);
    }
}