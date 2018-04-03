using System;

namespace AirTrafficMonitor.Converting
{
    public class StringToDateTimeConversion : IStringToDateTimeConversion
    {
        //https://stackoverflow.com/questions/919244/converting-a-string-to-datetime
        public DateTime ConvertToDateTime(string dateTimeAsString)
        {
            var year = dateTimeAsString.Substring(0, 4);
            var month = dateTimeAsString.Substring(4, 2);
            var date = dateTimeAsString.Substring(6, 2);
            var hours = dateTimeAsString.Substring(8, 2);
            var minutes = dateTimeAsString.Substring(10, 2);
            var seconds = dateTimeAsString.Substring(12, 2);
            var miliseconds = dateTimeAsString.Substring(14, 3);

            var outputDateTime = DateTime.ParseExact($"{year}-{month}-{date} {hours}:{minutes}:{seconds}:{miliseconds}",
                "yyyy-MM-dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);
            return outputDateTime;
        }
    }
}