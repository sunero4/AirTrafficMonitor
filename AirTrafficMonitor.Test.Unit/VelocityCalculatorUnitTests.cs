using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.VelocityCalc;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    public class VelocityCalculatorUnitTests
    {
        private VelocityCalculator _uut;
        private Track _track1;
        private Track _track2;
        private List<Track> _tracks;
        [SetUp]
        public void Setup()
        {
            _uut = new VelocityCalculator();

            _track1 = new Track()
            {
                Altitude = 12000,
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
                Altitude = 8000,
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

        }

        [Test]
        public void CalculateVelocity_ListWithTwoTracks_VelocityCalculated()
        {
            _uut.CalculateVelocity(_tracks);

            Assert.That(_tracks[1].Velocity,Is.EqualTo(87));
        }


    }
}
