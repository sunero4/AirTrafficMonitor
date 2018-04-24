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
    public class AirspaceMonitoringUnitTests
    {
        private AirspaceMonitoring _uut;
        private Airspace _airspace;
        private IAirspaceMovementMonitoring _airspaceMovementMonitoring;
        private Coordinates _southWestCorner;
        private Coordinates _northEastCorner;
        [SetUp]
        public void Setup()
        {
            _southWestCorner = new Coordinates(){X = 10000,Y = 10000};
            _northEastCorner = new Coordinates(){X = 90000, Y = 90000};
          
            _airspace = new Airspace(_southWestCorner, _northEastCorner, 500, 20000);
            _airspaceMovementMonitoring = Substitute.For<IAirspaceMovementMonitoring>();
            _uut = new AirspaceMonitoring(_airspace, _airspaceMovementMonitoring);
        }

        [TestCase(10000,40000,5000,true)]
        [TestCase(90000,45000,6000,true)]
        [TestCase(55000,10000,7000,true)]
        [TestCase(75000,90000,8000,true)]
        [TestCase(35000,25000,500,true)]
        [TestCase(65000,40000,20000,true)]
        [TestCase(9999,30000,2000,false)]
        [TestCase(90001,35000,3000,false)]
        [TestCase(20000,90001,10000,false)]
        [TestCase(60000,9999,15000,false)]
        [TestCase(30000,15000,499,false)]
        [TestCase(40000,25000,20001,false)]
        public void IsPlaneInAirspace_CheckCordinates_ReturnIfPlaneIsInAirspace(int planeX, int planeY, double planeAltitude, bool Result)
        {
            Coordinates planeCoordinates = new Coordinates(){X = planeX, Y = planeY};

            Assert.That(_uut.IsPlaneInAirspace(new Track() {Position = planeCoordinates, Altitude = planeAltitude}),Is.EqualTo(Result));
            
        }

        [Test]
        public void CheckIfPlaneIsInAirspace_PlaneIsInAirspaceEventIsInvoked_CorrectCallIsReceived()
        {
            var track = new Track() {Altitude = 5000, Position = new Coordinates() {X = 20000, Y = 20000}};
            _uut.CheckIfPlaneIsInAirspace(track);
            _airspaceMovementMonitoring.ReceivedWithAnyArgs().OnMovementInAirspaceDetected(_uut, new TrackEventArgs() {Track = track});
        }

        [Test]
        public void CheckIfPlaneIsInAirSpace_PlaneIsNotInAirspaceEventIsInvoked_CorrectCallIsReceived()
        {
            var track = new Track() {Altitude = 20, Position = new Coordinates() {X = 5, Y = 5}};
            _uut.CheckIfPlaneIsInAirspace(track);
            _airspaceMovementMonitoring.ReceivedWithAnyArgs().OnPlaneNotInAirspace(_uut, new TrackEventArgs() {Track = track});
        }
    }
}
