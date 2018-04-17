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
        private AirspaceMonitoring uut;
        private IAirspace _airspace;
        private Coordinates _southWestCorner;
        private Coordinates _northEastCorner;
        [SetUp]
        public void Setup()
        {
            _southWestCorner = new Coordinates(){X = 10000,Y = 10000};
            _northEastCorner = new Coordinates(){X = 90000, Y = 90000};
            uut = new AirspaceMonitoring();
            _airspace = Substitute.For<IAirspace>();
            _airspace.LowerAltitudeBoundary.Returns(500);
            _airspace.UpperAltitudeBoundary.Returns(20000);
            _airspace.SoutWestCorner.Returns(_southWestCorner);
            _airspace.NorthEastCorner.Returns(_northEastCorner);
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

            Assert.That(uut.IsPlaneInAirspace(_airspace,planeCoordinates,planeAltitude),Is.EqualTo(Result));
            
        }
    }
}
