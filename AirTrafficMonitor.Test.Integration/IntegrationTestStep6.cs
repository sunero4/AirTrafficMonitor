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
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep6
    {
        private TransponderDataReceiver _driver;
        private TransponderDataConversion _transponderDataConversion;
        private AirspaceMonitoring _airspaceMonitoring;
        private AirspaceMovementMonitoring _airspaceMovementMonitoring;
        private Airspace _airspace;
        private VelocityCalculator _velocityCalculator;
        private DegreesCalculatorWithoutDecimals _degreesCalculator;
        private ITrackLogging _trackLogging;
        private Track _track;
        private Separation _separation;
        private ITransponderReceiver _transponderReceiver;
        private ISeparationXmlLogging _separationXmlLogging;
        private ISeparationConsoleLogger _separationConsoleLogger;

        [SetUp]
        public void Setup()
        {
            _separationConsoleLogger = Substitute.For<ISeparationConsoleLogger>();
            _separationXmlLogging = Substitute.For<ISeparationXmlLogging>();
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _airspace = new Airspace(new Coordinates() { X = 10000, Y = 10000 }, new Coordinates() { X = 90000, Y = 90000 },
                500, 20000);
            _trackLogging = Substitute.For<ITrackLogging>();
            _degreesCalculator = new DegreesCalculatorWithoutDecimals();
            _velocityCalculator = new VelocityCalculator();
            _airspaceMovementMonitoring =
                new AirspaceMovementMonitoring(_airspace, _velocityCalculator, _degreesCalculator, _trackLogging);
            _airspaceMonitoring = new AirspaceMonitoring(_airspace, _airspaceMovementMonitoring);
            _transponderDataConversion = new TransponderDataConversion(_airspaceMonitoring);
            _separation = new Separation(_separationXmlLogging, _separationConsoleLogger);
            _driver = new TransponderDataReceiver(_transponderReceiver, _transponderDataConversion, _separation, _airspace);
            _track = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 50000,
                    Y = 60000
                },
                Tag = "XYZ123",
                TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56, 789)
            };          
        }


        [Test]
        public void 
    }
}
