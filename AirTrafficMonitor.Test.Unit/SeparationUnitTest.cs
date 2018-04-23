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
        private Track _track1;
        private Track _track2;
        private List<Track> _tracks;
        private Track _track3;
        private Track _track4;
        private Track _track5;
        private Track _track6;
        private Track _track7;
        private Track _track8;

        [SetUp]
        public void SetUp()
        {
            _separationXmlLoggingFake = Substitute.For<ISeparationXmlLogging>();
            _uut = new Separation(_separationXmlLoggingFake);

            _track1 = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 3000,
                    Y = 4000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840),

            };
            _track2 = new Track()
            {
                Altitude = 10301,
                Position = new Coordinates()
                {
                    X = 1000,
                    Y = 2000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 16, 55, 555),

            };
            _track3 = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 3000,
                    Y = 4000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840),

            };
            _track4 = new Track()
            {
                Altitude = 10300,
                Position = new Coordinates()
                {
                    X = 1000,
                    Y = 2000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 16, 55, 555),

            };
            _track5 = new Track()
            {
                Altitude = 11500,
                Position = new Coordinates()
                {
                    X = 3000,
                    Y = 4000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840),

            };
            _track6 = new Track()
            {
                Altitude = 10300,
                Position = new Coordinates()
                {
                    X = 1000,
                    Y = 2000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 16, 55, 555),

            };
            _track7 = new Track()
            {
                Altitude = 10000,
                Position = new Coordinates()
                {
                    X = 3000,
                    Y = 4000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840),

            };
            _track8 = new Track()
            {
                Altitude = 10300,
                Position = new Coordinates()
                {
                    X = 1000,
                    Y = 2000
                },
                Tag = "ABC987",
                TimeStamp = new DateTime(2013, 02, 20, 12, 16, 55, 555),

            };

            _tracks = new List<Track>();
            _tracks.Add(_track1);
            _tracks.Add(_track2);
            _tracks.Add(_track3);
            _tracks.Add(_track4);
            _tracks.Add(_track5);
            _tracks.Add(_track6);
            _tracks.Add(_track7);
            _tracks.Add(_track8);
        }

        [Test]
        public void Test()
        {

        }
    }
}
