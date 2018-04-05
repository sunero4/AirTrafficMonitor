using System;

namespace AirTrafficMonitor.Exceptions
{
    public class TransponderDataConversionException : Exception
    {
        public override string Message => "The input was invalid. Check amount of separators.";
    }
}
