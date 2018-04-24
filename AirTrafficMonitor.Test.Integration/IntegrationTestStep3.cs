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

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep3
    {

        private AirspaceMonitoring _driver;
        private AirspaceMovementMonitoring _airspaceMovementMonitoring;
        private VelocityCalculator _velocityCalculator;
        private DegreesCalculatorWithoutDecimals _degreesCalculator;
        private ITrackLogging _trackLogging;
        private Airspace _airspace;
        private Track _track1;


        [SetUp]
        public void Setup()
        {
            _airspace = new Airspace(new Coordinates() { X = 10000, Y = 10000 }, new Coordinates() { X = 90000, Y = 90000 },
                500, 20000);
            _velocityCalculator = new VelocityCalculator();
            _degreesCalculator = new DegreesCalculatorWithoutDecimals();
            _trackLogging = Substitute.For<ITrackLogging>();
            _airspaceMovementMonitoring =
                new AirspaceMovementMonitoring(_airspace, _velocityCalculator, _degreesCalculator, _trackLogging);
            _driver = new AirspaceMonitoring(_airspace,_airspaceMovementMonitoring);
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

        }

        [Test]
        public void OnMovementInAirspaceDetected_RaiseEvent_EventRaised()
        {
            bool wasRaised = false;

            _driver.PlaneIsInAirSpace += (o, e) => wasRaised = true;

            _driver.CheckIfPlaneIsInAirspace(_track1);

            Assert.That(wasRaised,Is.EqualTo(true));
        }

        [Test]
        public void NotInAirspace_RaiseEvent_EventRaised()
        {
            bool wasRaised = false;

            _track1.Position.X = 1000;


            _driver.PlaneIsNotInAirSpace += (o, e) => wasRaised = true;

            _driver.CheckIfPlaneIsInAirspace(_track1);

            Assert.That(wasRaised,Is.EqualTo(true));
        }


    }
}
