using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.CourseCalculations;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Logging;
using AirTrafficMonitor.VelocityCalc;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep3
    {
        private AirspaceMovementMonitoring _driver;
        private VelocityCalculator _velocityCalculator;
        private DegreesCalculatorWithoutDecimals _degreesCalculator;
        private ITrackLogging _trackLogging;
        private Airspace _airspace;
        private Track _track1;
        private Track _track2;
        private double _expectedCourse;

        [SetUp]
        public void Setup()
        {
            _airspace = new Airspace(new Coordinates(){X = 10000, Y = 10000}, new Coordinates(){X = 90000, Y = 90000}, 500, 20000);
            _velocityCalculator = new VelocityCalculator();
            _degreesCalculator = new DegreesCalculatorWithoutDecimals();
            _trackLogging = Substitute.For<ITrackLogging>();
            _driver = new AirspaceMovementMonitoring(_airspace, _velocityCalculator, _degreesCalculator, _trackLogging);
            _expectedCourse = 68;

            _track1 = new Track
            {
                Position = new Coordinates(){X = 30000, Y = 40000},
                Altitude = 10000,
                Tag = "AAA000",
                TimeStamp = new DateTime(2014, 08, 09, 15, 45, 20, 800)
            };

            _track2 = new Track
            {
                Position = new Coordinates() { X = 33500, Y = 48500 },
                Altitude = 9000,
                Tag = "AAA000",
                TimeStamp = new DateTime(2014, 08, 09, 16, 05, 45, 250)
            };

            _airspace.PlanesInAirspace.Add("AAA000", new List<Track>(){_track1});
        }

        [Test]
        public void CalculateDegrees()
        { 
            _driver.OnMovementInAirspaceDetected(_driver, new TrackEventArgs(){Track = _track2});

            Assert.That((_track2.Course), Is.EqualTo(_expectedCourse));
        }

        

}
}
