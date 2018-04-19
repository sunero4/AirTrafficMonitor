using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.AirspaceManagement;
using AirTrafficMonitor.Controllers;
using AirTrafficMonitor.Converting;
using AirTrafficMonitor.Domain;
using AirTrafficMonitor.Logging;
using AirTrafficMonitor.VelocityCalc;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitor.Test.Unit
{
    [TestFixture]
    class TransponderDataReceivedControllerUnitTests
    {
        private ITransponderReceiver _transponderReceiverFake;
        private ITransponderDataConversion _transponderDataConversionFake;
        private ITrackLogging _trackLogging;
        private IAirspaceMovementMonitoring _airspaceMovementMonitoring;
        private IAirspaceMonitoring _airspaceMonitoring;
        private TransponderDataReceivedController _uut;

        [SetUp]
        public void Setup()
        {
            _transponderReceiverFake = Substitute.For<ITransponderReceiver>();
            _transponderDataConversionFake = Substitute.For<ITransponderDataConversion>();
            _trackLogging = Substitute.For<ITrackLogging>();
            _airspaceMovementMonitoring = Substitute.For<IAirspaceMovementMonitoring>();
            _airspaceMonitoring = Substitute.For<IAirspaceMonitoring>();
            _uut = new TransponderDataReceivedController(_transponderReceiverFake, _transponderDataConversionFake, _trackLogging,_airspaceMovementMonitoring,_airspaceMonitoring);
        }

        [Test]
        public void StartReceivingTransponderData_OnTransponderDataReadySubscribesToEvent_CorrectCallIsReceived()
        {
            _uut.StartReceivingTransponderData();
            _transponderReceiverFake.Received().TransponderDataReady += _uut.OnTransponderDataReady;
        }

        [Test]
        public void StopReceivingTransponderData_OnTransponderDataReadyUnsubscribesFromEvent_CorrectCallIsReceived()
        {
            _uut.StopReceivingTransponderData();
            _transponderReceiverFake.Received().TransponderDataReady -= _uut.OnTransponderDataReady;
        }

        [Test]
        public void OnTransPonderDataReady_CallsConversionAndTrackLogging_CorrectMethodCallsAreReceived()
        {
            Coordinates position = new Coordinates() { X = 14000, Y = 15000 };
            var fakeData = new List<string>() {"test"};
            _transponderDataConversionFake.ConvertData(fakeData[0]).Returns(new Track()
                {
                    Altitude = 1000,
                    Position = new Coordinates() { X = 50000, Y = 50000},
                    Tag = "XYZ123",
                    TimeStamp = DateTime.Now,
                    Velocity = 300
                }
            );
            _airspaceMonitoring.IsPlaneInAirspace(new Coordinates() { X = 50000, Y = 50000 },1000).ReturnsForAnyArgs(true);
            _uut.OnTransponderDataReady(this, new RawTransponderDataEventArgs(fakeData));
            

            //Two received in one test, but method only executes properly if both are received, so it's okay
            //I guess
            _transponderDataConversionFake.Received().ConvertData("test");
            _trackLogging.ReceivedWithAnyArgs().LogTrack(new Track());
        }

        [Test]
        public void OnTransponderDataReady_AirplaneIsNotInAirspace_CallsCorrectMethodOnAirspaceMovementMonitoring()
        {
            _airspaceMonitoring.IsPlaneInAirspace(new Coordinates(), 1000).ReturnsForAnyArgs(false);
            _transponderDataConversionFake.ConvertData("test").Returns(new Track()
            {
                Altitude = 10000,
                Position = new Coordinates() { X = 1, Y = 2 },
                Tag = "ABC123",
                TimeStamp = DateTime.Now,
                Velocity = 400
            });
            _uut.OnTransponderDataReady(this, new RawTransponderDataEventArgs(new List<string>() { "test" }));
            _airspaceMovementMonitoring.ReceivedWithAnyArgs().OnPlaneNotInAirspace(this, new TrackEventArgs());
        }
    }
}
