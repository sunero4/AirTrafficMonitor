using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.VelocityCalc;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    class AirspaceMovementMonitoringUnitTests
    {
        private AirspaceMovementMonitoring _uut;
        private IAirspace _airspace;
        private IVelocityCalculator _velocityCalculatorFake;
        private Track _track;
        [SetUp]
        public void Setup()
        {
            _airspace = new Airspace(new Coordinates() {X = 10000, Y = 10000}, new Coordinates() {X = 90000, Y = 90000}, 500, 10000);
            _velocityCalculatorFake = Substitute.For<IVelocityCalculator>();
            _uut = new AirspaceMovementMonitoring(_airspace, _velocityCalculatorFake);
            _track = new Track()
            {
                Altitude = 1000,
                Position = new Coordinates() { X = 50000, Y = 50000 },
                Tag = "XYZ123",
                TimeStamp = DateTime.Now,
                Velocity = 300
            };
        }

        [Test]
        public void OnMovementInAirspaceDetected_PlaneIsNotAlreadyInAirspace_PlaneTrackGetsAddedToAirSpace()
        {

            _uut.OnMovementInAirspaceDetected(this, new TrackEventArgs() {Track = _track});
            Assert.That(_airspace.PlanesInAirspace.ContainsKey(_track.Tag));
        }

        [Test]
        public void OnMovementInAirspaceDetected_PlaneIsAlreadyInAirspace_PlaneTrackGetsAddedToExistingListInAirspace()
        {
            _airspace.PlanesInAirspace.Add(_track.Tag, new List<Track>() {_track});
            var track2 = new Track()
            {
                Altitude = 2000,
                Position = new Coordinates() {X = 40000, Y = 40000},
                Tag = "XYZ123",
                TimeStamp = DateTime.Now,
                Velocity = 400
            };
            _uut.OnMovementInAirspaceDetected(this, new TrackEventArgs() {Track = track2});
            Assert.That(_airspace.PlanesInAirspace[_track.Tag].Count, Is.EqualTo(2));
        }
    }
}
