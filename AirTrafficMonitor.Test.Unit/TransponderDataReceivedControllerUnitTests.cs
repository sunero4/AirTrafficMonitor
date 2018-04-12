using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private TransponderDataReceivedController _uut;

        [SetUp]
        public void Setup()
        {
            _transponderReceiverFake = Substitute.For<ITransponderReceiver>();
            _transponderDataConversionFake = Substitute.For<ITransponderDataConversion>();
            _trackLogging = Substitute.For<ITrackLogging>();
            _uut = new TransponderDataReceivedController(_transponderReceiverFake, _transponderDataConversionFake, _trackLogging, new Airspace(new VelocityCalculator()));
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
        public void OnTransPonderDataReceived_CallsConversionAndTrackLogging_CorrectMethodCallsAreReceived()
        {
            var fakeData = new List<string>() {"test"};
            _uut.OnTransponderDataReady(this, new RawTransponderDataEventArgs(fakeData));

            //Two received in one test, but method only executes properly if both are received, so it's okay
            //I guess
            _transponderDataConversionFake.Received().ConvertData("test");
            _trackLogging.ReceivedWithAnyArgs().LogTrack(new Track());
        }
    }
}
