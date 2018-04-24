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
using NUnit;
using NUnit.Framework; 

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep1
    {
        private AirspaceMovementMonitoring _driver;
        private VelocityCalculator _velocityCalculator;
        private IDegreesCalculator _degreesCalculator;
        private ITrackLogging _trackLogging;
        private Airspace _airspace;
        private Track _track1;
        private Track _track2;


        [SetUp]
        public void Setup()
        {
            _airspace = new Airspace(new Coordinates() {X = 10000, Y = 10000}, new Coordinates() {X = 90000, Y = 90000},
                500, 20000);
            _degreesCalculator = Substitute.For<IDegreesCalculator>();
            _trackLogging = Substitute.For<ITrackLogging>();
            _velocityCalculator = new VelocityCalculator();
            _driver = new AirspaceMovementMonitoring(_airspace, _velocityCalculator, _degreesCalculator, _trackLogging);

            _track1 = new Track()
            {
                Altitude = 12000,
                Position = new Coordinates()
                {
                    X = 30000,
                    Y = 40000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840),

            };
            _airspace.PlanesInAirspace.Add(_track1.Tag, new List<Track>() {_track1});

            _track2 = new Track()
            {
                Altitude = 8000,
                Position = new Coordinates()
                {
                    X = 10000,
                    Y = 20000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 16, 55, 555),
            };
        }
        [Test]
        public void CalculateVelocity()
        {
             _driver.OnMovementInAirspaceDetected(_driver, new TrackEventArgs() {Track = _track2});
            
            Assert.That(_track2.Velocity,Is.EqualTo(621));
        }
    }
}
