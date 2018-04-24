using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Logging;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    public class SeparationUnitTest
    {
        private Separation _uut;
        private ISeparationXmlLogging _separationXmlLoggingFake;
        private ISeparationConsoleLogger _separationConsoleLoggerFake;
        private Track _track1;
        private Track _track2;
        private Track _track3;
        private Track _track4;
        private List<Track> _tracks1;
        private List<Track> _tracks2;
        private Dictionary<string, List<Track>> PlanesInAirspace;

        [SetUp]
        public void SetUp()
        {
            _separationXmlLoggingFake = Substitute.For<ISeparationXmlLogging>();
            _separationConsoleLoggerFake = Substitute.For<ISeparationConsoleLogger>();
            _uut = new Separation(_separationXmlLoggingFake, _separationConsoleLoggerFake);
            PlanesInAirspace = new Dictionary<string, List<Track>>();

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
            _track2 = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 12000,
                    Y = 12000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 16, 55, 555),
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
            _track4 = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 12000,
                    Y = 12000
                },
                Tag = "ABC986",
                TimeStamp = new DateTime(2013, 02, 20, 12, 16, 55, 555),
            };
            
            _tracks1 = new List<Track>();
            _tracks1.Add(_track1);
            _tracks1.Add(_track2);
            PlanesInAirspace.Add("ABC987", _tracks1);
            _tracks2 = new List<Track>();
            _tracks2.Add(_track3);
            _tracks2.Add(_track4);
            PlanesInAirspace.Add("ABC986", _tracks2);
        }

        [TestCase(20000, 24999, 30000, 30000, 12000, 12000)]
        [TestCase(30000, 30000, 30000, 34999, 12000, 12000)]
        [TestCase(20000, 20000, 30000, 30000, 12000, 12299)]
        public void DetermineSeparationEvent_SeparationEventIsRaised_EventIsRaised(double X1, double X2, double Y1, double Y2, double alt1, double alt2)
        {
            bool wasRaised = false;

            _uut.SeparationEvent += (o, e) => wasRaised = true;

            PlanesInAirspace.ElementAt(0).Value[1].Position.X = X1;
            PlanesInAirspace.ElementAt(1).Value[1].Position.X = X2;
            PlanesInAirspace.ElementAt(0).Value[1].Position.Y = Y1;
            PlanesInAirspace.ElementAt(1).Value[1].Position.Y = Y2;
            PlanesInAirspace.ElementAt(0).Value[1].Altitude = alt1;
            PlanesInAirspace.ElementAt(1).Value[1].Altitude = alt2;

            _uut.MonitorSeparation(PlanesInAirspace);

            Assert.IsTrue(wasRaised);
        }

        [TestCase(20000, 25000, 30000, 30000, 12000, 12000)]
        [TestCase(30000, 30000, 30000, 35000, 12000, 12000)]
        [TestCase(20000, 20000, 30000, 30000, 12000, 12300)]
        public void DetermineSeparationEvent_SeparationEventIsNotRaised_EventIsNotRaised(double X1, double X2, double Y1, double Y2, double alt1, double alt2)
        {
            bool wasRaised = false;

            _uut.SeparationEvent += (o, e) => wasRaised = true;

            PlanesInAirspace.ElementAt(0).Value[1].Position.X = X1;
            PlanesInAirspace.ElementAt(1).Value[1].Position.X = X2;
            PlanesInAirspace.ElementAt(0).Value[1].Position.Y = Y1;
            PlanesInAirspace.ElementAt(1).Value[1].Position.Y = Y2;
            PlanesInAirspace.ElementAt(0).Value[1].Altitude = alt1;
            PlanesInAirspace.ElementAt(1).Value[1].Altitude = alt2;

            _uut.MonitorSeparation(PlanesInAirspace);

            Assert.IsFalse(wasRaised);
        }
    }
}
