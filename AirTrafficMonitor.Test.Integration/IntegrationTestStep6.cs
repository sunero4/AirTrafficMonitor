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
        private Separation _separation;
        private ITransponderReceiver _transponderReceiver;
        private ISeparationXmlLogging _separationXmlLogging;
        private ISeparationConsoleLogger _separationConsoleLogger;
        private Track _track1;
        private Track _track3;
        private List<Track> _tracks1;
        private List<Track> _tracks2;
        private Dictionary<string, List<Track>> _planesInAirspace;

        

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
            _planesInAirspace = new Dictionary<string, List<Track>>();

            _track1 = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 12000,
                    Y = 12000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840),

            };
            _track3 = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 12000,
                    Y = 12000
                },
                Tag = "ABC986",
                TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840),

            };
            

            _tracks1 = new List<Track>();
            _tracks1.Add(_track1);
            
            _airspace.PlanesInAirspace.Add("ABC987", _tracks1);
            _tracks2 = new List<Track>();
            _tracks2.Add(_track3);
            
            _airspace.PlanesInAirspace.Add("ABC986",_tracks2);
        }


        [Test]
        public void OnTransponderDataReady_()
        {
            bool wasRaised = false;


            string data1 = "ABC987;20000;30000;12000;20151006213456789";
            string data2 = "ABC986;24999;30000;12000;20151006213456789";


            _separation.SeparationEvent += (o, e) => wasRaised = true;

            _driver.OnTransponderDataReady(_driver, new RawTransponderDataEventArgs(new List<string>() { data1,data2 }));

            Assert.That(wasRaised,Is.EqualTo(true));
        }
           
    }
}
