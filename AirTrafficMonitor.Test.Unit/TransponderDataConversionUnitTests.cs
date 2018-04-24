using System;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    public class TransponderDataConversionUnitTests
    {
        public class TestData
        {
            public Track ExpectedTrack { get; set; }
            public string TransponderData { get; set; }
        }
        private IAirspaceMonitoring _airspaceMonitoringFake;
        private TransponderDataConversion _uut;

        //Using ValueSource as only primitive types can be passed in TestCases
        private static TestData[] _validTestData = new TestData[]
        {
            new TestData()
            {
                ExpectedTrack = new Track()
                {
                    Altitude = 10000,
                    Position = new Coordinates()
                    {
                        X = 5000,
                        Y = 6000
                    },
                    Tag = "XYZ123",
                    TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56, 789)
                },
                TransponderData = "XYZ123;5000;6000;10000;20151006213456789"
            },
            new TestData()
            {
                ExpectedTrack = new Track()
                {
                    Altitude = 9999,
                    Position = new Coordinates()
                    {
                        X = 0,
                        Y = 0
                    },
                    Tag = "ABCD1234",
                    TimeStamp = new DateTime(2018, 12, 07, 11, 23, 59, 123)
                },
                TransponderData = "ABCD1234;0;0;9999;20181207112359123"
            }
        };

        [SetUp]
        public void Setup()
        {
            //Datetime conversion is tested separately in StringToDateTimeConversionUnitTests.cs
            _airspaceMonitoringFake = Substitute.For<IAirspaceMonitoring>();
            _uut = new TransponderDataConversion(_airspaceMonitoringFake);
        }

        [Test]
        public void ConvertRawDataToTrack_ValidStringData_ReturnsTrackWithCorrectAltitude([ValueSource(nameof(_validTestData))]TestData testData)
        {
            var convertedData = _uut.ConvertRawDataToTrack(testData.TransponderData);
            Assert.That(convertedData.Altitude, Is.EqualTo(testData.ExpectedTrack.Altitude));
        }

        [Test]
        public void ConvertRawDataToTrack_ValidStringData_ReturnsTrackWithCorrectXCoordinate(
            [ValueSource(nameof(_validTestData))] TestData testData)
        {
            var convertedData = _uut.ConvertRawDataToTrack(testData.TransponderData);
            Assert.That(convertedData.Position.X, Is.EqualTo(testData.ExpectedTrack.Position.X));
        }

        [Test]
        public void ConvertRawDataToTrack_ValidStringData_ReturnsTrackWithCorrectYCoordinate(
            [ValueSource(nameof(_validTestData))] TestData testData)
        {
            var convertedData = _uut.ConvertRawDataToTrack(testData.TransponderData);
            Assert.That(convertedData.Position.Y, Is.EqualTo(testData.ExpectedTrack.Position.Y));
        }

        [Test]
        public void ConvertRawDataToTrack_ValidStringData_ReturnsTrackWithCorrectTag(
            [ValueSource(nameof(_validTestData))] TestData testData)
        {
            var convertedData = _uut.ConvertRawDataToTrack(testData.TransponderData);
            Assert.That(convertedData.Tag, Is.EqualTo(testData.ExpectedTrack.Tag));
        }

        [Test]
        public void ConvertData_CallsAirspaceMonitoring_CorrectMethodCallIsReceived()
        {
            _uut.ConvertData("ABCD1234;0;0;9999;20181207112359123");
            _airspaceMonitoringFake.ReceivedWithAnyArgs().CheckIfPlaneIsInAirspace(new Track());
        }

        [Test]
        public void ConvertData_InvalidAmountOfSemiColons_ThrowsTransponderDataConversionException()
        {
            Assert.Throws<TransponderDataConversionException>(() => _uut.ConvertData("ABCD1234;100;9999;20181207112359123"));
        }
    }
}
