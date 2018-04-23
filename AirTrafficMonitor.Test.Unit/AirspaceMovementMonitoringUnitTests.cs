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

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    class AirspaceMovementMonitoringUnitTests
    {
        private AirspaceMovementMonitoring _uut;
        private Airspace _airspace;
        private IVelocityCalculator _velocityCalculatorFake;
        private IDegreesCalculator _degreesCalculatorFake;
        private ITrackLogging _trackLoggingFake;
        private Track _track;
        [SetUp]
        public void Setup()
        {
            //Airspace only consists of properties, so there would be no real benefit of faking it.
            //For maximum testability we could have chosen to change the Dictionary property to an IDictionary
            //or IEnumerable<KeyValuePair<string, List<Track> and injected a fake there, but we'll assume
            //C#'s collections are working as intended.
            _airspace = new Airspace(new Coordinates() {X = 10000, Y = 10000}, new Coordinates() {X = 90000, Y = 90000}, 500, 10000);
            _velocityCalculatorFake = Substitute.For<IVelocityCalculator>();
            _degreesCalculatorFake = Substitute.For<IDegreesCalculator>();
            _trackLoggingFake = Substitute.For<ITrackLogging>();
            _uut = new AirspaceMovementMonitoring(_airspace, _velocityCalculatorFake, _degreesCalculatorFake, _trackLoggingFake);
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
        public void OnMovementInAirspaceDetected_PlaneIsAlreadyInAirspace_PlaneTrackGetsAddedToCorrectExistingListInAirspace()
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

        [Test]
        public void
            OnMovementInAirspaceDetected_PlaneIsNotAlreadyInAirspace_PlaneTrackGetsAddedToSeparateListInDictionary()
        {
            _airspace.PlanesInAirspace.Add(_track.Tag, new List<Track>() {_track});
            var track2 = new Track()
            {
                Altitude = 2000,
                Position = new Coordinates() { X = 40000, Y = 40000 },
                Tag = "ABC123",
                TimeStamp = DateTime.Now,
                Velocity = 400
            };
            _uut.OnMovementInAirspaceDetected(this, new TrackEventArgs() {Track = track2});

            Assert.That(_airspace.PlanesInAirspace[track2.Tag].Count, Is.EqualTo(1));
        }

        [Test]
        public void OnMovementInAirspaceDetected_PlaneIsAlreadyInAirspace_MethodCallToVelocityCalculatorIsReceived()
        {
            var trackList = new List<Track>() {_track};
            _airspace.PlanesInAirspace.Add(_track.Tag, trackList);
            _uut.OnMovementInAirspaceDetected(this, new TrackEventArgs() {Track = _track});
            _velocityCalculatorFake.Received().CalculateVelocity(trackList);
        }

        [Test]
        public void OnPlaneNotInAirSpace_PlaneWasInAirspace_PlaneIsRemovedFromAirspace()
        {
            _airspace.PlanesInAirspace.Add(_track.Tag, new List<Track>() {_track});
            _uut.OnPlaneNotInAirspace(this, new TrackEventArgs() {Track = _track});

            Assert.IsFalse(_airspace.PlanesInAirspace.ContainsKey(_track.Tag));
        }
    }
}
