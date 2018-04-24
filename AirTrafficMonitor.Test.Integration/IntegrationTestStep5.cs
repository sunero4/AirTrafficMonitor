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
using AirTrafficMonitor.VelocityCalc;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep5
    {
        private TransponderDataConversion _driver;
        private AirspaceMonitoring _airspaceMonitoring;
        private AirspaceMovementMonitoring _airspaceMovementMonitoring;
        private Airspace _airspace;
        private VelocityCalculator _velocityCalculator;
        private DegreesCalculatorWithoutDecimals _degreesCalculator;
        private ITrackLogging _trackLogging;
        private Track _track;

        [SetUp]
        public void Setup()
        {
            _airspace = new Airspace(new Coordinates() { X = 10000, Y = 10000 }, new Coordinates() { X = 90000, Y = 90000 },
                500, 20000);
            _trackLogging = Substitute.For<ITrackLogging>();
            _degreesCalculator = new DegreesCalculatorWithoutDecimals();
            _velocityCalculator = new VelocityCalculator();
            _airspaceMovementMonitoring =
                new AirspaceMovementMonitoring(_airspace, _velocityCalculator, _degreesCalculator, _trackLogging);
            _airspaceMonitoring = new AirspaceMonitoring(_airspace,_airspaceMovementMonitoring);
            _driver  =new TransponderDataConversion(_airspaceMonitoring);

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
        public void ConvertData_CheckIfPlaneIsInAirspace_ReturnsTrue()
        {
            string data = "XYZ123;50000;60000;10000;20151006213456789";

            bool wasRaised = false;
            _airspaceMonitoring.PlaneIsInAirSpace += (o, e) => wasRaised = true;

            _driver.ConvertData(data);

            Assert.That(wasRaised,Is.EqualTo(true));
        }



        






    }
}
