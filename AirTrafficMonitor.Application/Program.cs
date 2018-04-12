﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Controllers;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Logging;
using AirTrafficMonitor.Rendering;
using AirTrafficMonitor.VelocityCalc;
using TransponderReceiver;

namespace AirTrafficMonitor.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var transponderDataConversion = new TransponderDataConversion(new StringToDateTimeConversion());
            var trackStringRepresentation = new TrackToStringRepresentation();
            var trackLogger = new TrackConsoleLogging(trackStringRepresentation);
            var controller = new TransponderDataReceivedController(TransponderReceiverFactory.CreateTransponderDataReceiver(), transponderDataConversion, trackLogger, new Airspace(new VelocityCalculator()));
            controller.StartReceivingTransponderData();
            Console.ReadLine();
        }
    }
}
