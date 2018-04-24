using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.CourseCalculations;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Logging;
using AirTrafficMonitor.Receiving;
using AirTrafficMonitor.VelocityCalc;
using TransponderReceiver;

namespace AirTrafficMonitor.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var airspace = new Airspace(new Coordinates() { X = 10000, Y = 10000 },
                new Coordinates() { X = 90000, Y = 90000 }, 500, 20000);

            var trackLogging = new TrackConsoleLogging();
            var velocityCalculator = new VelocityCalculator();
            var degreesCalculator = new DegreesCalculatorWithoutDecimals();
            var airspaceMovementMonitoring = new AirspaceMovementMonitoring(airspace, velocityCalculator, degreesCalculator, trackLogging);

            var separationConsoleLogger = new SeparationConsoleLogger();
            var separationXmlLogger = new SeparationXmlLogging();

            var separation = new Separation(separationXmlLogger);
            var airspaceMonitoring = new AirspaceMonitoring(airspace, airspaceMovementMonitoring);
            var transponderDataConversion = new TransponderDataConversion(airspaceMonitoring);
            var transponderDataReceiver = new TransponderDataReceiver(
                TransponderReceiverFactory.CreateTransponderDataReceiver(), transponderDataConversion, separation, airspace);

            transponderDataReceiver.StartReceivingData();
            Console.ReadLine();
        }
    }
}
