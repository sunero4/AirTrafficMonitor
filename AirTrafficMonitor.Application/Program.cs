using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
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
            var airspace = new Airspace(new VelocityCalculator(), new Coordinates() {X = 10000, Y = 10000},
                new Coordinates() {X = 90000, Y = 90000}, 500, 20000);
            var controller = new TransponderDataReceivedController(TransponderReceiverFactory.CreateTransponderDataReceiver(), transponderDataConversion, trackLogger, new AirspaceMovementMonitoring(airspace, new VelocityCalculator()), new AirspaceMonitoring(airspace));
            controller.StartReceivingTransponderData();
            Console.ReadLine();
        }
    }
}
