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
    public class IntegrationTestStep5
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
        private ISeparation _separation;
        private ITransponderReceiver _transponderReceiver;


        [SetUp]
        public void Setup()
        {
            _separation = Substitute.For<ISeparation>();
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
            _driver = new TransponderDataReceiver(_transponderReceiver,_transponderDataConversion,_separation,_airspace);
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
        public void OnTransponderDataReady_ConvertData_DataConverted()
        {
            string data = "XYZ123;50000;60000;10000;20151006213456789";

            _driver.OnTransponderDataReady(_driver,new RawTransponderDataEventArgs(new List<string>(){data}));

            
        }


    }
}
