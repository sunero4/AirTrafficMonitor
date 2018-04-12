﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Rendering;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    public class TrackToStringRepresentationUnitTests
    {
        private TrackToStringRepresentation _uut;
        private ITransponderDataConversion _transponderData;

        public class TestData
        {
            public Track TestTrack { set; get; }
            public string StringTest { get; set; }
        }

        private static TestData[] _dataUnderTest = new TestData[]
        {
            new TestData()
            {
                TestTrack = new Track()
                {
                    Altitude = 8000,
                    Position = new Coordinates()
                    {
                        X = 1000,
                        Y = 2000
                    },
                    Tag = "ABC987",
                    TimeStamp = new DateTime(2012, 08, 15, 18, 45, 20, 555)
                },
                StringTest = "Tag: ABC987 \n" +
            "Position: X: 1000, Y: 2000 \n" +
            "Altitude: 8000 \n" +
            "Timestamp: 15-08-2012 18:45:20 \n"
            },
            new TestData()
            {
                TestTrack = new Track()
                {
                    Altitude = 12000,
                    Position = new Coordinates()
                    {
                        X = 3000,
                        Y = 4000
                    },
                    Tag = "DEF475",
                    TimeStamp = new DateTime(2013, 02, 20, 12, 15, 50, 840)
                },
                StringTest = "Tag: DEF475 \n" +
                    "Position: X: 3000, Y: 4000 \n" +
                    "Altitude: 12000 \n" +
                    "Timestamp: 20-02-2013 12:15:50 \n"
            }

        };

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackToStringRepresentation();
            _transponderData = Substitute.For<ITransponderDataConversion>();
        }

        [Test]
        public void ReturnTrackWithCorrectFormatdafdgaerg([ValueSource(nameof(_dataUnderTest))]TestData testData)
        {
            var stringTest = _uut.GenerateStringRepresentation(testData.TestTrack);
            Assert.That(stringTest, Is.EqualTo(testData.StringTest));
        }

    }
}