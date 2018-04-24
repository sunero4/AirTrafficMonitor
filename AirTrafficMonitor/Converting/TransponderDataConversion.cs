﻿using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Exceptions;

namespace AirTrafficMonitor.Converting
{
    public class TransponderDataConversion : ITransponderDataConversion
    {
        private readonly IStringToDateTimeConversion _stringToDateTimeConversion;
        private readonly IAirspaceMonitoring _airspaceMonitoring;

        public TransponderDataConversion(IStringToDateTimeConversion stringToDateTimeConversion, IAirspaceMonitoring airspaceMonitoring)
        {
            _stringToDateTimeConversion = stringToDateTimeConversion;
            _airspaceMonitoring = airspaceMonitoring;
        }

        public void ConvertData(string receivedData)
        {
            try
            {
                var convertedData = ConvertRawDataToTrack(receivedData);
                _airspaceMonitoring.CheckIfPlaneIsInAirspace(convertedData);
            }
            catch (IndexOutOfRangeException)
            {
                //Throwing custom exception with custom message.
                throw new TransponderDataConversionException();
            }
        }

        public Track ConvertRawDataToTrack(string transponderData)
        {
            var separatedValues = transponderData.Split(';');
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
    }
}