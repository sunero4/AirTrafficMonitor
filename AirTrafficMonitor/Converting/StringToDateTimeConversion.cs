using System;

namespace AirTrafficMonitor.Converting
{
    public class StringToDateTimeConversion : IStringToDateTimeConversion
    {
        //https://stackoverflow.com/questions/919244/converting-a-string-to-datetime
        public DateTime ConvertToDateTime(string dateTimeAsString)
        {
            var outputDateTime = DateTime.ParseExact(dateTimeAsString,
                "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture);
            return outputDateTime;
        }
    }
}