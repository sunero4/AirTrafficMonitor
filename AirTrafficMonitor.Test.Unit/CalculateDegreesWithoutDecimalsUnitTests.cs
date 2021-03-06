﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.CourseCalculations;
using AirTrafficMonitor.Domain;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    public class CalculateDegreesWithoutDecimalsUnitTests
    {
        private IDegreesCalculator _uut;
        private Coordinates _coord0;
        private Coordinates _coord1;

        [SetUp]
        public void Setup()
        {
            _uut = new DegreesCalculatorWithoutDecimals();
            _coord0 = new Coordinates();
            _coord1 = new Coordinates();
        }

        [TestCase(20000, 40000, 25000, 45000, 45)]
        [TestCase(40000, 25000, 33500, 32500, 131)]
        public void DegreeCalculationWithoutDecimalsTest(int x1, int y1, int x2, int y2, int degrees)
        {
            List<Track> testTracks = new List<Track>();

            _coord0.X = x1;
            _coord0.Y = y1;
            _coord1.X = x2;
            _coord1.Y = y2;

            Track testTrack1 = new Track
            {
                Position = _coord0,
                Altitude = 10000,
                Tag = "AAA000",
                TimeStamp = DateTime.Now,
                Velocity = 300
            };

            Track testTrack2 = new Track
            {
                Position = _coord1,
                Altitude = 10000,
                Tag = "AAA000",
                TimeStamp = DateTime.Now,
                Velocity = 300
            };

            testTracks.Add(testTrack1);
            testTracks.Add(testTrack2);

            _uut.CalculateDegrees(testTracks);

            Assert.That(testTrack2.Course, Is.EqualTo(degrees));
        }


    }
}
