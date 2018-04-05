using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Exceptions;

namespace AirTrafficMonitor.Converting
{
    public class TransponderDataConversion : ITransponderDataConversion
    {
        private readonly IStringToDateTimeConversion _stringToDateTimeConversion;
        public TransponderDataConversion(IStringToDateTimeConversion stringToDateTimeConversion)
        {
            _stringToDateTimeConversion = stringToDateTimeConversion;
        }

        public Track ConvertData(string receivedData)
        {
            var separatedValues = receivedData.Split(';');
            try
            {
                var convertedData = new Track()
                {
                    Tag = separatedValues[0],
                    Position = new Coordinates()
                    {
                        X = Convert.ToDouble(separatedValues[1]),
                        Y = Convert.ToDouble(separatedValues[2])
                    },
                    Altitude = Convert.ToDouble(separatedValues[3]),
                    TimeStamp = _stringToDateTimeConversion.ConvertToDateTime(separatedValues[4])
                };
                return convertedData;
            }
            catch (IndexOutOfRangeException e)
            {
                //Throwing custom exception with custom message.
                throw new TransponderDataConversionException();
            }
        }
    }
}