using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Extensions;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    class ListSlidingWindowExtensionUnitTests
    {
        private List<Track> _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new List<Track>();
        }

        [Test]
        public void AddToSlidingWindowList_ListCountIsBelowMaxSize_TrackIsAdded()
        {
            var track = new Track();
            _uut.AddToSlidingWindowList(track, 2);

            Assert.That(_uut.Contains(track));
        }

        [Test]
        public void AddToSlidingWindowList_ListCountIsMaxSize_FirstElementIsRemoved()
        {
            var track1 = new Track();
            var track2 = new Track();
            var track3 = new Track();

            _uut.Add(track1);
            _uut.Add(track2);
            _uut.AddToSlidingWindowList(track3, 2);

            Assert.IsFalse(_uut.Contains(track1));
        }

        [Test]
        public void AddToSlidingWindowList_ListCountIsMaxSize_ElementIsAddedAtCorrectPosition()
        {
            var track1 = new Track();
            var track2 = new Track();
            var track3 = new Track();

            _uut.Add(track1);
            _uut.Add(track2);
            _uut.AddToSlidingWindowList(track3, 2);

            Assert.That(_uut[1], Is.EqualTo(track3));
        }

    }
}
